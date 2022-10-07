namespace Dotnet9.Web.Caches;

public class RedisCacheService : ICacheService
{
    private readonly ConnectionMultiplexer _connection;

    private readonly string? _instance;
    protected IDatabase Cache;


    public RedisCacheService(RedisCacheOptions options, int database = 0)
    {
        _connection = ConnectionMultiplexer.Connect(options.Configuration!);
        Cache = _connection.GetDatabase(database);
        _instance = options.InstanceName;
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await Task.FromResult(Cache.KeyExists(GetKeyForRedis(key)));
    }

    public async Task<bool> AddAsync(string key, object value)
    {
        return await Task.FromResult(Cache.StringSet(GetKeyForRedis(key),
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value))));
    }

    public async Task<bool> AddAsync(string key, object value, TimeSpan expireSliding, TimeSpan expireAbsoulte)
    {
        return await Task.FromResult(Cache.StringSet(GetKeyForRedis(key),
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)),
            expireAbsoulte));
    }

    public async Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
    {
        return await Task.FromResult(Cache.StringSet(GetKeyForRedis(key),
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)),
            expiresIn));
    }

    public async Task<bool> RemoveAsync(string key)
    {
        return await Task.FromResult(Cache.KeyDelete(GetKeyForRedis(key)));
    }

    public async Task RemoveAllAsync(IEnumerable<string> keys)
    {
        foreach (var key in keys) await RemoveAsync(key);
    }

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        var value = Cache.StringGet(GetKeyForRedis(key));
        await Task.CompletedTask;
        
        return !value.HasValue ? default : JsonConvert.DeserializeObject<T>(value!);
    }

    public async Task<object?> GetAsync(string key)
    {
        var value = Cache.StringGet(GetKeyForRedis(key));
        await Task.CompletedTask;

        return !value.HasValue ? null : JsonConvert.DeserializeObject(value!);
    }

    public async Task<IDictionary<string, object?>> GetAllAsync(IEnumerable<string> keys)
    {
        var dict = new Dictionary<string, object?>();
        foreach (var key in keys) dict.Add(key, await GetAsync(GetKeyForRedis(key)));

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

    public string GetKeyForRedis(string key)
    {
        return _instance + key;
    }

    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}