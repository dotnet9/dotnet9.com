namespace Dotnet9.WebAPI.Domain;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<AboutDomainService>();
        services.AddScoped<ActionLogDomainService>();
    }
}