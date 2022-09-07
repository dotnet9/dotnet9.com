using Dotnet9.WebAPI.Infrastructure.UserAdmin;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.WebAPI.Infrastructure;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IdDomainService>();
        services.AddScoped<IIdRepository, IdRepository>();
    }
}