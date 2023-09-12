using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Caching.Distributed;

namespace Dotnet9.Services.Config.Services;

[Inject(InjectType.Scope)]
public class ConfigService
{
    private readonly IDistributedCache _cache;
    private readonly DbContext _context;

    public ConfigService(DbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<string> GetAsync(string key)
    {
        string? cacheValue = await _cache.GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            cacheValue = await _context.Set<SysConfig>().Where(a => a.Key == key).Select(a => a.Value)
                .FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(cacheValue) == false)
            {
                await _cache.SetStringAsync(key, cacheValue);
            }
        }

        return cacheValue!;
    }


    public async Task<string> GetAsync(string key, string defaultValue)
    {
        string cacheValue = await GetAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            return defaultValue;
        }

        return cacheValue;
    }


    public async Task<int> GetAsync(string key, int defaultValue)
    {
        string cacheValue = await GetAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            return defaultValue;
        }

        return Convert.ToInt32(cacheValue);
    }

    public async Task<bool> GetAsync(string key, bool defaultValue)
    {
        string cacheValue = await GetAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            return defaultValue;
        }

        return Convert.ToBoolean(cacheValue);
    }

    /// <summary>
    ///     更新配置
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public async Task UpdateConfig(Dictionary<string, string> dic)
    {
        List<SysConfig> list = await _context.Set<SysConfig>().ToListAsync();
        foreach ((string k, string v) in dic)
        {
            SysConfig? item = list.FirstOrDefault(a => a.Key == k);
            if (item == null)
            {
                _context.Set<SysConfig>().Add(new SysConfig { Key = k, Value = v });
                _cache.SetString($"{k}", v);
            }
            else
            {
                item.Value = v;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<T> Get<T>() where T : class, new()
    {
        T t = new T();


        foreach (PropertyInfo property in t.GetType().GetProperties())
        {
            string key = $"{t.GetType().Name}:{property.Name}";
            string? value = await _cache.GetStringAsync(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                value = await _context.Set<SysConfig>().Where(a => a.Key == key).Select(a => a.Value)
                    .FirstOrDefaultAsync();
                if (value != null)
                {
                    await _cache.SetStringAsync(key, value);
                }
            }

            if (value != null)
            {
                property.SetValue(t, Convert.ChangeType(value, property.PropertyType));
            }
        }

        return t;
    }

    #region 获取Key，强类型

    public async Task<string> GetString<T>(Expression<Func<T, object>> exp, string defaultValue = "")
        where T : class, new()
    {
        string key = ConfigHelper.GetK(exp);
        return await GetAsync(key, defaultValue);
    }

    public async Task<int> GetInt<T>(Expression<Func<T, object>> exp, int defaultValue = 0) where T : class, new()
    {
        string key = ConfigHelper.GetK(exp);
        return await GetAsync(key, defaultValue);
    }

    public async Task<bool> GetBool<T>(Expression<Func<T, object>> exp, bool defaultValue = false)
        where T : class, new()
    {
        string key = ConfigHelper.GetK(exp);
        return await GetAsync(key, defaultValue);
    }

    #endregion
}