using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Web.Caches;
using Dotnet9.Web.Utils;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Dotnet9.Web.ServiceExtensions;

public static class CacheSetup
{
    public static void AddCacheSetup(this IServiceCollection services)
    {
        services.AddMemoryCache();
        if (GlobalVar.Cache!.IsRedis)
        {
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
            {
                Configuration = GlobalVar.Cache!.RedisConnection,
                InstanceName = GlobalVar.Cache!.InstanceName
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