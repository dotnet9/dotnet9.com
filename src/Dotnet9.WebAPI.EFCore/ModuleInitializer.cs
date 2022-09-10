namespace Dotnet9.WebAPI.EFCore;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IdDomainService>();
        services.AddScoped<IIdRepository, IdRepository>();
        services.AddScoped<IAboutRepository, AboutRepository>();
    }
}