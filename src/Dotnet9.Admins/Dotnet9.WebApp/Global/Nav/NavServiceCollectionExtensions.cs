// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class NavServiceCollectionExtensions
{
    public static IServiceCollection AddNav(this IServiceCollection services, List<NavModel> navList)
    {
        services.AddSingleton(navList);
        services.AddScoped<NavHelper>();

        return services;
    }

    public static IServiceCollection AddNav(this IServiceCollection services, string navSettingsFile)
    {
        var navList = JsonSerializer.Deserialize<List<NavModel>>(File.ReadAllText(navSettingsFile));

        if (navList is null) throw new Exception("请首先配置导航文件!");

        services.AddNav(navList);

        return services;
    }
}