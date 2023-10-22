using EasyCaching.Core;

namespace SqlSugar;

/// <summary>
/// SqlSugar ORM框架所需缓存配置
/// </summary>
public class SqlSugarCache : ICacheService
{
    private static readonly IEasyCachingProvider Cache = App.GetRequiredService<IEasyCachingProvider>();

    public void Add<V>(string key, V value)
    {
        Cache.Set(key, value, TimeSpan.MaxValue);
    }

    public void Add<V>(string key, V value, int cacheDurationInSeconds)
    {
        Cache.Set(key, value, TimeSpan.FromSeconds(cacheDurationInSeconds));
    }

    public bool ContainsKey<V>(string key)
    {
        return Cache.Exists(key);
    }

    public V Get<V>(string key)
    {
        return Cache.Get<V>(key).Value;
    }

    public IEnumerable<string> GetAllKey<V>()
    {
        return Cache.GetByPrefix<object>("SqlSugarDataCache.").Keys;
    }

    public void Remove<V>(string key)
    {
        Cache.Remove(key);
    }

    public V GetOrCreate<V>(string cacheKey, Func<V> create, int cacheDurationInSeconds = 2147483647)
    {
        if (Cache.Exists(cacheKey))
        {
            return Cache.Get<V>(cacheKey).Value;
        }

        V v = create();
        Cache.Set(cacheKey, v, TimeSpan.FromSeconds(cacheDurationInSeconds));
        return v;
    }
}