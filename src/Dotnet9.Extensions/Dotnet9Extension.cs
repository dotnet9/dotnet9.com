using Dotnet9.Extensions.Caches;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Dotnet9.Extensions;

public static class Dotnet9Extension
{
    public static void AddExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddCacheSetup(builder.Configuration.GetSection("Cache").Get<CacheConfig>()!);
    }
}