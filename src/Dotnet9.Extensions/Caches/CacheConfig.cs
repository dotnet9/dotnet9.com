namespace Dotnet9.Extensions.Caches;

public class CacheConfig
{
    public string RedisConnection { get; set; } = null!;

    public string InstanceName { get; set; } = null!;

    public bool IsRedis { get; set; }
}