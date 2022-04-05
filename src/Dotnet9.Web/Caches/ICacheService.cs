namespace Dotnet9.Web.Caches;

public interface ICacheService
{
    Task<bool> ExistsAsync(string key);

    Task<bool> AddAsync(string key, object value);
    Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
    Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false);

    Task<bool> RemoveAsync(string key);
    Task RemoveAllAsync(IEnumerable<string> keys);

    Task<T?> GetAsync<T>(string key) where T : class;
    Task<object?> GetAsync(string key);
    Task<IDictionary<string, object?>> GetAllAsync(IEnumerable<string> keys);
    Task<bool> ReplaceAsync(string key, object value);
    Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
    Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false);
}