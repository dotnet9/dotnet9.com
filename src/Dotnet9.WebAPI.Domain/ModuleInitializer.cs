namespace Dotnet9.WebAPI.Domain;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<AboutManager>();
        services.AddScoped<ActionLogManager>();
        services.AddScoped<CategoryManager>();
        services.AddScoped<AlbumManager>();
    }
}