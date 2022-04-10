using Dotnet9.Application.Contracts.Caches;

namespace Dotnet9.Web.Caches;

public class CacheHelper
{
    public static ICacheService? Cache { get; set; }
}