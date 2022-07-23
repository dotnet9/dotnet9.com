using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dotnet9.WebApp.MasaExtensions;

public static class MasaSetup
{
    public static void AddMasaSetup(this IServiceCollection services, bool isServer = true, string? baseUri = null)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddMasaBlazor(builder =>
        {
            builder.Theme.Primary = "#4318FF";
            builder.Theme.Accent = "#4318FF";
        });
        services.TryAddScoped<I18n>();
        services.TryAddScoped<CookieStorage>();
        services.AddHttpContextAccessor();
        if (isServer || string.IsNullOrWhiteSpace(baseUri))
            services.AddGlobalForServer();
        else
            _ = services.AddGlobalForWasmAsync(baseUri);
    }
}