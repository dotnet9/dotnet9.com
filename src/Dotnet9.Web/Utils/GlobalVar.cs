using Dotnet9.Web.Caches;

namespace Dotnet9.Web.Utils;

public static class GlobalVar
{
    public static string? SiteDomain { get; set; }
    public static string? AssetsLocalPath { get; set; }
    public static string? AssetsRemotePath { get; set; }
    public static CacheConfig? Cache { get; set; }
}