using Dotnet9.Application.Contracts.Caches;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.Extensions.Caches;

public static class CacheSetup
{
    public static void AddCacheSetup(this IServiceCollection services, CacheConfig cacheConfig)
    {
        services.AddMemoryCache();
        if (cacheConfig.IsRedis)
        {
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
            {
                Configuration = cacheConfig.RedisConnection,
                InstanceName = cacheConfig.InstanceName
            }));
        }
        else
        {
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddSingleton<ICacheService, MemoryCacheService>();
        }
    }
}