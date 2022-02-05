using Dotnet9.Extensions.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.Extensions.ServiceExtensions;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(AutoMapperConfig));
    }
}