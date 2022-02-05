using Microsoft.Extensions.Caching.Memory;

namespace Dotnet9.Common.MemoryCache;

public class MemoryCachingProvider : ICachingProvider
{
    private readonly IMemoryCache _cache;

    public MemoryCachingProvider(IMemoryCache cache)
    {
        _cache = cache;
    }

    public object Get(string cacheKey)
    {
        return _cache.Get(cacheKey);
    }

    public void Set(string cacheKey, object cacheValue)
    {
        _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(7200));
    }
}