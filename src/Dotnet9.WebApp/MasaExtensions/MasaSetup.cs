using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.WebApp.MasaExtensions;

public static class MasaSetup
{
    public static void AddMasaSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddMasaBlazor();
    }
}