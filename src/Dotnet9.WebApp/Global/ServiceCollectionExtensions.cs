// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalForServer(this IServiceCollection services)
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                       throw new Exception("获取程序集根目录异常！");
        services.AddNav(Path.Combine(basePath, "wwwroot/nav/nav.json"));
        services.AddScoped<GlobalConfig>();

        return services;
    }

    public static IServiceCollection AddGlobalForWasmAsync(this IServiceCollection services, string baseUri)
    {
        Task.Run(async () =>
        {
            using var httpClient = new HttpClient();
            var navList = await httpClient.GetFromJsonAsync<List<NavModel>>(Path.Combine(baseUri, "nav/nav.json")) ??
                          throw new Exception("请首先配置导航文件!");
            services.AddNav(navList);
        });
        services.AddScoped<GlobalConfig>();

        return services;
    }
}