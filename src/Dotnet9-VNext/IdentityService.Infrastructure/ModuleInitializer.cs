namespace IdentityService.Infrastructure;

public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IdDomainService>();
        services.AddScoped<IIdRepository, IdRepository>();
    }
}