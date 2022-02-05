namespace Dotnet9.Common.MemoryCache;

public interface ICachingProvider
{
    object Get(string cacheKey);

    void Set(string cacheKey, object cacheValue);
}