using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Dotnet9.Common.Helpers;

public class Appsettings
{
    public Appsettings(string contentPath)
    {
        var path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

        Configuration = new ConfigurationBuilder()
            .SetBasePath(contentPath)
            .Add(new JsonConfigurationSource
            {
                Path = path,
                Optional = false,
                ReloadOnChange = true
            })
            .Build();
    }

    public Appsettings(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private static IConfiguration Configuration { get; set; }
    private static string? contentPath { get; set; }

    public static string App(params string[] sections)
    {
        try
        {
            if (sections.Any()) return Configuration[string.Join(":", sections)];
        }
        catch (Exception)
        {
            // ignored
        }

        return string.Empty;
    }

    public static List<T> App<T>(params string[] sections)
    {
        var list = new List<T>();
        Configuration.Bind(string.Join(":", sections), list);
        return list;
    }
}