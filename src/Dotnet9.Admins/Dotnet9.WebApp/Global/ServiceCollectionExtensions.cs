// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalForServer(this IServiceCollection services)
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                       throw new Exception("获取程序集根目录异常！");

#if DEBUG
        var configFile = "wwwroot/nav/nav.json";
#else
        var configFile = "wwwroot/_content/Dotnet9.WebApp/nav/nav.json";
#endif

        services.AddNav(Path.Combine(basePath, configFile));

        services.AddScoped<GlobalConfig>();

        return services;
    }

    public static IServiceCollection AddGlobalForWasmAsync(this IServiceCollection services, string baseUri)
    {
        Task.Run(async () =>
        {
            using var httpClient = new HttpClient();


#if DEBUG
            var configFile = "wwwroot/nav/nav.json";
#else
            var configFile = "_content/Dotnet9.WebApp/nav/nav.json";
#endif

            var navList =
                await httpClient.GetFromJsonAsync<List<NavModel>>(Path.Combine(baseUri, configFile)) ??
                throw new Exception("请首先配置导航文件!");
            services.AddNav(navList);
        });
        services.AddScoped<GlobalConfig>();

        return services;
    }
}