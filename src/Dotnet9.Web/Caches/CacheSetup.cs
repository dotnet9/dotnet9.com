namespace Dotnet9.Web.Caches;

public static class CacheSetup
{
    public static void AddCacheSetup(this IServiceCollection services, CacheSettings cacheSettings)
    {
        services.AddMemoryCache();
        if (cacheSettings.IsRedis)
        {
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
            {
                Configuration = cacheSettings.RedisConnection,
                InstanceName = cacheSettings.InstanceDBName
            }));
        }
        else
        {
            services.AddSingleton<IMemoryCache>(_ => new MemoryCache(new MemoryCacheOptions()));
            services.AddSingleton<ICacheService, MemoryCacheService>();
        }
    }
}