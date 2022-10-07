namespace Dotnet9.Web.Settings;

public class CacheSettings
{
    public bool IsRedis { get; set; }

    public string? InstanceDBName { get; set; }

    public string? RedisConnection { get; set; }
}