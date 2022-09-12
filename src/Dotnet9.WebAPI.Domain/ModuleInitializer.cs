namespace Dotnet9.WebAPI.Domain;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IdManager>();
        services.AddScoped<AboutManager>();
        services.AddScoped<ActionLogManager>();
        services.AddScoped<CategoryManager>();
        services.AddScoped<AlbumManager>();
        services.AddScoped<TagManager>();
        services.AddScoped<BlogPostManager>();
        services.AddScoped<DonationManager>();
    }
}