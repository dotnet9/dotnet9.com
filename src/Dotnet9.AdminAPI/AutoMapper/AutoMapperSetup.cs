using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.AdminAPI.AutoMapper;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(AutoMapperConfig));
    }
}