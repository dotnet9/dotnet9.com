using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Dotnet9.Core.Const;
using Dotnet9.Core.Entities;
using Dotnet9.Core.Enum;
using Dotnet9.Core.Options;
using Mapster;
using Newtonsoft.Json;
using Yitter.IdGenerator;

namespace SqlSugar;

public static class SqlSugarExtensions
{
    /// <summary>
    /// 添加 SqlSugar 拓展（适用于单个数据库）
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config">数据库连接对象</param>
    /// <param name="buildAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugar(this IServiceCollection services, ConnectionConfigExt config,
        Action<ISqlSugarClient> buildAction = default)
    {
        return services.AddSqlSugar(new List<ConnectionConfigExt>()
        {
            config
        }, buildAction);
    }

    /// <summary>
    /// 添加 SqlSugar 拓展（适用于多数据库）
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configs">数据库连接对象集合</param>
    /// <param name="buildAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugar(this IServiceCollection services, List<ConnectionConfigExt> configs,
        Action<SqlSugarClient> buildAction = default)
    {
        SqlSugarScope sqlSugarScope = new SqlSugarScope(configs.Adapt<List<ConnectionConfig>>(), buildAction ?? Aop);
        configs.ForEach(x => { Init(sqlSugarScope, x); });
        services.AddSingleton<ISqlSugarClient>(sqlSugarScope); //使用单例注入
        services.AddSingleton<ITenant>(sqlSugarScope);
        services.AddUnitOfWork<SqlSugarUnitOfWork>(); //事务
        services.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>)); //注入泛型仓储

        return services;
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="client"></param>
    /// <param name="config"></param>
    static void Init(SqlSugarScope client, ConnectionConfigExt config)
    {
        if (!config.EnableInitDb)
        {
            return;
        }

        SqlSugarScopeProvider provider = client.GetConnectionScope(config.ConfigId);
        //创建数据库
        provider.DbMaintenance.CreateDatabase();
        IEnumerable<Type> types = App.EffectiveTypes.Where(x =>
            !x.IsInterface && x.IsClass && !x.IsAbstract && typeof(IEntity).IsAssignableFrom(x));
        provider.CodeFirst.InitTables(types.ToArray());

        //初始化数据
        InitData(client);
    }

    /// <summary>
    /// 添加 SqlSugar 服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugar(this IServiceCollection services)
    {
        var options = App.GetOptions<DbConnectionOptions>();
        ICacheService ormCache = new SqlSugarCache();
        options.Connections.ForEach(x =>
        {
            x.MoreSettings = new ConnMoreSettings()
            {
                //所有 增、删 、改 会自动调用.RemoveDataCache()
                IsAutoRemoveDataCache = true
            };
            //配置缓存
            x.ConfigureExternalServices = new ConfigureExternalServices()
            {
                DataInfoCacheService = ormCache,
                EntityService = ((prop, column) =>
                {
                    //如果实体不是主键，并且是可空类型，表列设置为可空(支持string?和string)
                    if (column.IsPrimarykey == false && new NullabilityInfoContext()
                            .Create(prop).WriteState is NullabilityState.Nullable)
                    {
                        column.IsNullable = true;
                    }
                })
            };
        });
        return AddSqlSugar(services, options.Connections, Aop);
    }

    /// <summary>
    /// 拦截器
    /// </summary>
    private static readonly Action<SqlSugarClient> Aop = (db) =>
    {
        db.Aop.DataExecuting = (_, entityInfo) =>
        {
            switch (entityInfo.OperationType)
            {
                //执行insert时
                case DataFilterType.InsertByObject:
                    //自动设置主键
                    if (entityInfo.EntityColumnInfo.IsPrimarykey &&
                        entityInfo.EntityValue is Entity<long> { Id: 0 } entity)
                    {
                        entity.Id = YitIdHelper.NextId();
                    }

                    //如果当前实体继承ICreatedTime就设置创建时间
                    if (entityInfo.EntityValue is ICreatedTime createdTime &&
                        createdTime.CreatedTime == DateTime.MinValue)
                    {
                        createdTime.CreatedTime = DateTime.Now;
                    }

                    if (entityInfo.EntityValue is ICreatedUserId { CreatedUserId: 0 } createdUserId)
                    {
                        createdUserId.CreatedUserId =
                            App.User.FindFirst(AuthClaimsConst.AuthIdKey)!.Value.Adapt<long>();
                    }

                    break;
                case DataFilterType.UpdateByObject:
                    if (entityInfo.EntityValue is IUpdatedTime { UpdatedTime: null } updatedTime)
                    {
                        updatedTime.UpdatedTime = DateTime.Now;
                    }

                    break;
                case DataFilterType.DeleteByObject:
                    break;
            }
        };

        // 文档地址：https://www.donet5.com/Home/Doc?typeId=1204
        db.Aop.OnLogExecuting = (sql, parameters) =>
        {
            var originColor = Console.ForegroundColor;
            if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                Console.ForegroundColor = ConsoleColor.Green;
            if (sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) ||
                sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("【" + DateTime.Now + "——执行SQL】\r\n" +
                              UtilMethods.GetSqlString(db.CurrentConnectionConfig.DbType, sql, parameters) + "\r\n");
            Console.ForegroundColor = originColor;
            App.PrintToMiniProfiler("SqlSugar", "Info",
                sql + "\r\n" +
                db.Utilities.SerializeObject(parameters.ToDictionary(it => it.ParameterName, it => it.Value)));
        };
        db.Aop.OnError = ex =>
        {
            if (ex.Parametres == null) return;
            var originColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            var pars = db.Utilities.SerializeObject(
                ((SugarParameter[])ex.Parametres).ToDictionary(it => it.ParameterName, it => it.Value));
            Console.WriteLine("【" + DateTime.Now + "——错误SQL】\r\n" +
                              UtilMethods.GetSqlString(db.CurrentConnectionConfig.DbType, ex.Sql,
                                  (SugarParameter[])ex.Parametres) + "\r\n");
            Console.ForegroundColor = originColor;
            App.PrintToMiniProfiler("SqlSugar", "Error",
                $"{ex.Message}{Environment.NewLine}{ex.Sql}{pars}{Environment.NewLine}");
        };

        //配置逻辑删除过滤器（查询数据时过滤掉已被标记删除的数据）
        db.QueryFilter.AddTableFilter<ISoftDelete>(it => it.DeleteMark == false);
    };

    /// <summary>
    /// 初始化基础数据
    /// </summary>
    private static void InitData(SqlSugarScope client)
    {
        List<SysUser> users = new List<SysUser>()
        {
            new SysUser()
            {
                Id = 1,
                Account = "admin",
                Password = "9658b68df5e6208e97ae6c8f4eac6c39",
                Name = "管理员",
                Gender = Gender.Female,
                NickName = "管理员",
                Avatar = "https://oss.okay123.top/oss//2023/07/13/Yln0lzZQLN.jpg",
                CreatedUserId = 1
            },
            new SysUser()
            {
                Id = 50048753934341,
                Account = "test",
                Password = "65caa9533b8d9f336bd07919dd9bf74e",
                Name = "测试",
                Gender = Gender.Female,
                NickName = "测试",
                CreatedUserId = 1
            }
        };
        client.Storageable(users).ToStorage().AsInsertable.ExecuteCommand();

        string path = Path.Combine(AppContext.BaseDirectory, "InitData");
        var dir = new DirectoryInfo(path);
        var files = dir.GetFiles("*.json").ToList();
        InitDataFromFile(client, users[0]);
        foreach (var file in files)
        {
            using var reader = file.OpenText();
            string s = reader.ReadToEnd();
            var table = JsonConvert.DeserializeObject<DataTable>(s);
            if (table.Rows.Count == 0)
            {
                continue;
            }

            table.TableName = file.Name.Replace(".json", "");

            client.Storageable(table).WhereColumns("Id").ToStorage().AsInsertable.ExecuteCommand();
        }
    }

    /// <summary>
    /// 获取初始化文件列表
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static void InitDataFromFile(SqlSugarScope client, SysUser user)
    {
        var siteOptions = App.GetConfig<SiteOptions>("Site");
        if (string.IsNullOrWhiteSpace(siteOptions.AssetsDir) || !Directory.Exists(siteOptions.AssetsDir))
        {
            throw new Exception("请配置资源目录");
        }

        InitFriendLink(client, siteOptions.AssetsDir);
        InitCategory(client, siteOptions.AssetsDir, user);
        InitAlbum(client, siteOptions.AssetsDir, user);
    }

    /// <summary>
    /// 初始化友情链接数据
    /// </summary>
    /// <param name="client"></param>
    /// <param name="assetsDir"></param>
    /// <exception cref="Exception"></exception>
    private static void InitFriendLink(SqlSugarScope client, string assetsDir)
    {
        var filePath = Path.Combine(assetsDir, "site", "FriendLink.json");
        if (!File.Exists(filePath))
        {
            throw new Exception($"请配置友情链接文件：{filePath}");
        }

        var friendLinks = JsonConvert.DeserializeObject<List<FriendLink>>(File.ReadAllText(filePath));
        var id = 0;
        friendLinks.ForEach(link =>
        {
            link.Id = ++id;
            link.Link = link.Url;
        });
        client.Storageable(friendLinks).ToStorage().AsInsertable.ExecuteCommand();
    }

    /// <summary>
    /// 初始化分类
    /// </summary>
    /// <param name="client"></param>
    /// <param name="assetsDir"></param>
    /// <exception cref="Exception"></exception>
    private static void InitCategory(SqlSugarScope client, string assetsDir, SysUser user)
    {
        void UpdateCategory(List<Categories> all, Categories category, string assetsUrl, ref long id)
        {
            if (category.Children.Count <= 0)
            {
                return;
            }

            foreach (var cat in category.Children)
            {
                cat.Id = ++id;
                cat.ParentId = category.Id;
                cat.CreatedUserId = user.Id;
                cat.Cover = $"{assetsUrl}/{cat.Cover}";
                all.Add(cat);

                UpdateCategory(all, cat, assetsUrl, ref id);
            }
        }

        var filePath = Path.Combine(assetsDir, "cats", "category.json");
        if (!File.Exists(filePath))
        {
            throw new Exception($"请配置分类文件：{filePath}");
        }

        var siteOptions = App.GetConfig<SiteOptions>("Site");
        if (string.IsNullOrWhiteSpace(siteOptions.AssetsUrl))
        {
            throw new Exception("请配置资源Url");
        }

        var categories = JsonConvert.DeserializeObject<List<Categories>>(File.ReadAllText(filePath));
        var allCategories = new List<Categories>();


        long id = 0;
        foreach (var cat in categories)
        {
            cat.Id = ++id;
            cat.ParentId = null;
            cat.CreatedUserId = user.Id;
            cat.Cover = $"{siteOptions.AssetsUrl}/{cat.Cover}";
            allCategories.Add(cat);

            UpdateCategory(allCategories, cat, siteOptions.AssetsUrl, ref id);
        }

        client.Storageable(allCategories).ToStorage().AsInsertable.ExecuteCommand();
    }

    /// <summary>
    /// 初始化专辑
    /// </summary>
    /// <param name="client"></param>
    /// <param name="assetsDir"></param>
    /// <exception cref="Exception"></exception>
    private static void InitAlbum(SqlSugarScope client, string assetsDir, SysUser user)
    {
        var filePath = Path.Combine(assetsDir, "albums", "album.json");
        if (!File.Exists(filePath))
        {
            throw new Exception($"请配置专辑文件：{filePath}");
        }

        var siteOptions = App.GetConfig<SiteOptions>("Site");
        if (string.IsNullOrWhiteSpace(siteOptions.AssetsUrl))
        {
            throw new Exception("请配置资源Url");
        }

        var albums = JsonConvert.DeserializeObject<List<Albums>>(File.ReadAllText(filePath));


        long id = 0;
        foreach (var album in albums)
        {
            album.Id = ++id;
            album.CreatedUserId = user.Id;
            album.Cover = $"{siteOptions.AssetsUrl}/{album.Cover}";
        }

        client.Storageable(albums).ToStorage().AsInsertable.ExecuteCommand();
    }
}