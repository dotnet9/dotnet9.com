namespace Dotnet9.Application.Contracts.Caches;

public interface ICacheService
{
    Task<bool> ExistsAsync(string key);

    Task<bool> AddAsync(string key, object value);
    Task<bool> AddAsync(string key, object value, TimeSpan expireSliding, TimeSpan expireAbsoulte);
    Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false);

    Task<bool> RemoveAsync(string key);
    Task RemoveAllAsync(IEnumerable<string> keys);

    Task<T?> GetAsync<T>(string key) where T : class;
    Task<object?> GetAsync(string key);
    Task<IDictionary<string, object?>> GetAllAsync(IEnumerable<string> keys);

    Task<bool> ReplaceAsync(string key, object value);
    Task<bool> ReplaceAsync(string key, object value, TimeSpan expireSliding, TimeSpan expireAbsoulte);
    Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false);

    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int expireSlidingSecond = 5 * 60,
        int expireAbsoulteSeconds = 30 * 60) where T : class;
}