using Dotnet9.Common.MemoryCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.Extensions.ServiceExtensions;

public static class MemoryCacheSetup
{
    public static void AddMemoryCacheSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddScoped<ICachingProvider, MemoryCachingProvider>();
        services.AddSingleton<IMemoryCache>(factory =>
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            return cache;
        });
    }
}