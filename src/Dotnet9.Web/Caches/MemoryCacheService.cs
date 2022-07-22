namespace Dotnet9.Web.Caches;

public class MemoryCacheService : ICacheService
{
    protected IMemoryCache Cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        Cache = cache;
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await Task.FromResult(Cache.TryGetValue(key, out _));
    }


    public async Task<bool> AddAsync(string key, object value)
    {
        Cache.Set(key, value);
        return await ExistsAsync(key);
    }

    public async Task<bool> AddAsync(string key, object value, TimeSpan expireSliding, TimeSpan expireAbsoulte)
    {
        Cache.Set(key, value,
            new MemoryCacheEntryOptions()
                .SetSlidingExpiration(expireSliding)
                .SetAbsoluteExpiration(expireAbsoulte)
        );

        return await ExistsAsync(key);
    }

    public async Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
    {
        if (isSliding)
            Cache.Set(key, value,
                new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(expiresIn)
            );
        else
            Cache.Set(key, value,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expiresIn)
            );

        return await ExistsAsync(key);
    }


    public async Task<bool> RemoveAsync(string key)
    {
        Cache.Remove(key);

        return await ExistsAsync(key);
    }

    public async Task RemoveAllAsync(IEnumerable<string> keys)
    {
        keys.ToList().ForEach(item => Cache.Remove(item));
        await Task.CompletedTask;
    }


    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        return await Task.FromResult(Cache.Get(key) as T);
    }

    public async Task<object?> GetAsync(string key)
    {
        return await Task.FromResult(Cache.Get(key));
    }

    public async Task<IDictionary<string, object?>> GetAllAsync(IEnumerable<string> keys)
    {
        var dict = new Dictionary<string, object?>();

        keys.ToList().ForEach(item => dict.Add(item, Cache.Get(item)));
        await Task.CompletedTask;

        return dict;
    }

    public async Task<bool> ReplaceAsync(string key, object value)
    {
        if (!await ExistsAsync(key)) return await AddAsync(key, value);

        if (!await RemoveAsync(key))
            return false;

        return await AddAsync(key, value);
    }

    public async Task<bool> ReplaceAsync(string key, object value, TimeSpan expireSliding, TimeSpan expireAbsoulte)
    {
        if (!await ExistsAsync(key)) return await AddAsync(key, value, expireSliding, expireAbsoulte);

        if (!await RemoveAsync(key))
            return false;

        return await AddAsync(key, value, expireSliding, expireAbsoulte);
    }

    public async Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
    {
        if (!await ExistsAsync(key)) return await AddAsync(key, value, expiresIn, isSliding);

        if (!await RemoveAsync(key))
            return false;

        return await AddAsync(key, value, expiresIn, isSliding);
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int expireSlidingSeconds,
        int expireAbsoulteSeconds) where T : class
    {
        if (!await ExistsAsync(key))
        {
            var value = await factory();
            await AddAsync(key, value, TimeSpan.FromSeconds(expireSlidingSeconds),
                TimeSpan.FromSeconds(expireAbsoulteSeconds));
            return value;
        }

        return await GetAsync<T>(key);
    }

    public void Dispose()
    {
        Cache.Dispose();
        GC.SuppressFinalize(this);
    }
}