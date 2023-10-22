using Dotnet9.Application.Config.Dtos;
using Furion.ViewEngine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Dotnet9.Application.Config;

/// <summary>
/// 自定义配置业务
/// </summary>
public class CustomConfigService : BaseService<CustomConfig>, ITransient
{
    private readonly ISqlSugarRepository<CustomConfig> _customConfigRepository;
    private readonly IEasyCachingProvider _easyCachingProvider;
    private readonly IViewEngine _viewEngine;
    private readonly IWebHostEnvironment _environment;

    public CustomConfigService(ISqlSugarRepository<CustomConfig> customConfigRepository,
        IEasyCachingProvider easyCachingProvider,
        IViewEngine viewEngine,
        IWebHostEnvironment environment) : base(customConfigRepository)
    {
        _customConfigRepository = customConfigRepository;
        _easyCachingProvider = easyCachingProvider;
        _viewEngine = viewEngine;
        _environment = environment;
    }

    /// <summary>
    /// 获取强类型配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [NonAction]
    public async Task<T> Get<T>()
    {
        var type = typeof(T);
        bool isList = typeof(IEnumerable).IsAssignableFrom(type);
        string code = type.IsGenericType && isList ? type.GenericTypeArguments[0].Name : type.Name;
        var value = await _easyCachingProvider.GetAsync($"{CacheConst.ConfigCacheKey}{code}", async () =>
        {
            var queryable = _customConfigRepository.AsQueryable()
                .InnerJoin<CustomConfigItem>((config, item) => config.Id == item.ConfigId)
                .Where((config, item) => config.Code == code)
                .Select((config, item) => item.Json);
            string json;
            if (isList)
            {
                List<string> list = await queryable.ToListAsync();
                if (!list.Any()) return default(T);
                json = $"[{string.Join(",", list)}]";
            }
            else
            {
                json = await queryable.FirstAsync();
            }

            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }, TimeSpan.FromDays(1));
        return value.Value;
    }

    /// <summary>
    /// 获取自定义配置
    /// </summary>
    /// <param name="code">自定义配置唯一编码</param>
    /// <returns></returns>
    [DisplayName("获取自定义配置")]
    [HttpGet("getConfig")]
    public async Task<dynamic> GetConfig([FromQuery][Required(ErrorMessage = "缺少参数")] string code)
    {
        var value = await _easyCachingProvider.GetAsync<object>($"{CacheConst.ConfigCacheKey}{code}", async () =>
        {
            var c = await _customConfigRepository.GetFirstAsync(x => x.Code == code);
            if (c == null) return null;
            var queryable = _customConfigRepository.AsQueryable()
                .InnerJoin<CustomConfigItem>((config, item) => config.Id == item.ConfigId)
                .Where((config, item) => config.Code == code)
                .Select((config, item) => item.Json);
            if (c.IsMultiple)
            {
                List<string> list = await queryable.ToListAsync();
                if (!list.Any()) return null;
                return JArray.Parse($"[{string.Join(",", list)}]");
            }

            string s = await queryable.FirstAsync();
            return string.IsNullOrWhiteSpace(s) ? null : JObject.Parse(s);
        }, TimeSpan.FromDays(1));
        return value.Value;
    }

    /// <summary>
    /// 自定义配置分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("自定义配置分页查询")]
    [HttpGet]
    public async Task<PageResult<CustomConfigPageOutput>> Page([FromQuery] CustomConfigQueryInput dto)
    {
        return await _customConfigRepository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Code), x => x.Code.Contains(dto.Code))
            .OrderByDescending(x => x.Id)
            .Select(x => new CustomConfigPageOutput
            {
                Id = x.Id,
                Status = x.Status,
                Remark = x.Remark,
                Name = x.Name,
                Code = x.Code,
                IsMultiple = x.IsMultiple,
                AllowCreationEntity = x.AllowCreationEntity,
                IsGenerate = x.IsGenerate,
                CreatedTime = x.CreatedTime
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 添加自定义配置
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加自定义配置")]
    [HttpPost("add")]
    public async Task AddConfig(AddCustomConfigInput dto)
    {
        if (await _customConfigRepository.IsAnyAsync(x => x.Code == dto.Code))
        {
            throw Oops.Bah("编码已存在");
        }
        var config = dto.Adapt<CustomConfig>();
        await _customConfigRepository.InsertAsync(config);
        await _easyCachingProvider.RemoveByPrefixAsync($"{CacheConst.ConfigCacheKey}{config.Code}");
    }

    /// <summary>
    /// 修改自定义配置
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("修改自定义配置")]
    [HttpPut("edit")]
    public async Task UpdateConfig(UpdateCustomConfigInput dto)
    {
        var config = await _customConfigRepository.GetByIdAsync(dto.Id);
        if (config == null)
        {
            throw Oops.Bah("无效参数");
        }
        if (dto.Code != config.Code && await _customConfigRepository.IsAnyAsync(x => x.Code == dto.Code && x.Id != dto.Id))
        {
            throw Oops.Bah("编码已存在");
        }

        dto.Adapt(config);
        await _customConfigRepository.UpdateAsync(config);
        await _easyCachingProvider.RemoveByPrefixAsync($"{CacheConst.ConfigCacheKey}{config.Code}");
    }

    /// <summary>
    /// 获取配置表单设计和表单数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [DisplayName("获取配置表单设计和表单数据")]
    [HttpGet("GetFormJson")]
    public async Task<CustomConfigDetailOutput> GetFormJson([FromQuery] long id, [FromQuery] long? itemId)
    {
        var output = new CustomConfigDetailOutput()
        {
            ItemId = itemId ?? 0
        };
        string json = await _customConfigRepository.AsQueryable()
                 .Where(x => x.Id == id)
                 .Select(x => x.Json).FirstAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            return output;
        };
        output.FormJson = JObject.Parse(json);
        if (itemId == 0) return output;
        var queryable = _customConfigRepository.AsSugarClient().Queryable<CustomConfigItem>();
        var data = itemId.HasValue ? await queryable.Where(x => x.Id == itemId).Select(x => new { x.Id, x.Json }).FirstAsync()
            : await queryable.Where(x => x.ConfigId == id).Select(x => new { x.Id, x.Json }).FirstAsync();
        if (data != null)
        {
            output.DataJson = JObject.Parse(data.Json);
            output.ItemId = data.Id;
        }
        return output;
    }

    /// <summary>
    /// 修改配置表单设计
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("修改配置表单设计")]
    [HttpPatch]
    public async Task SetJson(CustomConfigSetJsonInput dto)
    {
        string json = JsonConvert.SerializeObject(dto.Json);
        await _customConfigRepository.UpdateSetColumnsTrueAsync(x => new CustomConfig()
        {
            Json = json
        }, x => x.Id == dto.Id);
        await ClearCache();
    }

    /// <summary>
    /// 生成自定配置类
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("生成自定配置类")]
    [HttpPost]
    public async Task Generate(KeyDto dto)
    {
        if (!_environment.IsDevelopment())
        {
            throw Oops.Bah("生成配置类仅限开发环境使用");
        }
        var config = await _customConfigRepository.GetByIdAsync(dto.Id);
        if (config == null) throw Oops.Bah("无效参数");
        if (string.IsNullOrWhiteSpace(config.Json)) throw Oops.Bah("请配置设计");
        var controls = ResolveJson(config.Json);
        if (!controls.Any()) throw Oops.Bah("请配置设计");
        await GenerateCode(config.Code, controls);
        await _customConfigRepository.UpdateAsync(x => new CustomConfig()
        {
            IsGenerate = true
        }, x => x.Id == config.Id);
        await _easyCachingProvider.RemoveByPrefixAsync($"{CacheConst.ConfigCacheKey}{config.Code}");
    }

    /// <summary>
    /// 删除自定义配置类
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("删除自定义配置类")]
    [HttpPatch("deleteClass")]
    public async Task DeleteClass(KeyDto dto)
    {
        if (!_environment.IsDevelopment())
        {
            throw Oops.Bah("删除配置类仅限开发环境使用");
        }

        string className = await _customConfigRepository.AsQueryable().Where(x => x.Id == dto.Id).Select(x => x.Code).FirstAsync();
        if (className == null) throw Oops.Oh("无效参数");
        string path = Path.Combine(_environment.ContentRootPath.Replace(_environment.ApplicationName, ""), "Dotnet9.Core/Config", $"{className}.cs");
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        await _customConfigRepository.UpdateAsync(x => new CustomConfig() { IsGenerate = false }, x => x.Id == dto.Id);
        await _easyCachingProvider.RemoveByPrefixAsync($"{CacheConst.ConfigCacheKey}{className}");
    }

    internal override Task ClearCache()
    {
        return _easyCachingProvider.RemoveByPrefixAsync(CacheConst.ConfigCacheKey);
    }

    /// <summary>
    /// 解析表单设计
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    [NonAction]
    public List<CustomControl> ResolveJson(string json)
    {
        string s = "{\"key\":\\d+,\"type\":\"(input|select|date|switch|number|textarea|radio|checkbox|time|time-range|date-range|rate|color|slider|cascader|rich-editor|file-upload|picture-upload)\".*?\"id\".*?}";
        string value = string.Join(",", Regex.Matches(json, s, RegexOptions.IgnoreCase).Select(x => x.Value));
        string temp = $"[{value}]";
        return JsonConvert.DeserializeObject<List<CustomControl>>(temp);
    }

    /// <summary>
    /// 生成类文件
    /// </summary>
    /// <param name="className">类名</param>
    /// <param name="options"></param>
    [NonAction]
    public async Task GenerateCode(string className, List<CustomControl> options)
    {
        if (!options.Any()) return;
        List<string> fields = new List<string>();
        options.ForEach(x =>
        {
            string type = "string";
            switch (x.Type.ToLower())
            {
                case "select":
                case "checkbox":
                case "cascader":
                    if (x.Options.Multiple || "checkbox".Equals(x.Type, StringComparison.CurrentCultureIgnoreCase))
                    {
                        type = "List<string>";
                    }
                    break;
                case "date":
                    type = x.Options.Required ? "DateTime" : "DateTime?";
                    break;
                case "switch":
                    type = x.Options.Required ? "bool" : "bool?";
                    break;
                case "number":
                    type = x.Options.Precision > 0 ? "double" : "int";
                    type += x.Options.Required ? string.Empty : "?";
                    break;
                case "time":
                    type = x.Options.Required ? "TimeOnly" : "TimeOnly?";
                    break;
                case "time-range":
                    type = "List<TimeOnly>";
                    break;
                case "date-range":
                    type = "List<DateTime>";
                    break;
                case "rate":
                    type = x.Options.AllowHalf ? "double" : "int";
                    type += x.Options.Required ? string.Empty : "?";
                    break;
                case "file-upload":
                case "picture-upload":
                    type = x.Options.Limit > 1 ? "List<string>" : "string";
                    break;
                default:
                    type = "string";
                    break;
            }

            string remark = x.Options.OptionItems?.Any() ?? false
                ? string.Join("；", x.Options.OptionItems.Select(s => $"{s.Value}:{s.Label}"))
                : string.Empty;
            string template = @$"
    /// <summary>
    /// {x.Options.Label}{remark}
    /// </summary>
    public {type} {x.Options.Name} {{ get; set; }}";
            fields.Add(template);
        });

        string template = @"
using System;
using System.Collections.Generic;
namespace Dotnet9.Core.Config;
public class @Model.ClassName
{@foreach(var item in Model.Items)
    {
        @item
    }

}
";
        string content = await _viewEngine.RunCompileFromCachedAsync(template, new { ClassName = className, Items = fields });
        string path = Path.Combine(_environment.ContentRootPath.Replace(_environment.ApplicationName, ""), "Dotnet9.Core/Config");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fileName = Path.Combine(path, $"{className}.cs");
        if (System.IO.File.Exists(fileName))
        {
            System.IO.File.Delete(fileName);
        }
        await System.IO.File.WriteAllTextAsync(fileName, content);
    }
}