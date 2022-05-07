using Dotnet9.Extensions.AutoMapper;
using Dotnet9.Extensions.Caches;
using Dotnet9.Extensions.CountSystemInfo;
using Dotnet9.Extensions.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Dotnet9.Extensions;

public static class Dotnet9Extension
{
    public static void AddExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddCacheSetup(builder.Configuration.GetSection("Cache").Get<CacheConfig>()!);
        builder.Services.AddAutoMapperSetup();
        builder.Services.AddRepositorySetup();
        builder.Services.ConfigureNonBreakingSameSiteCookies();
        PerfCounter.Init();
    }
}