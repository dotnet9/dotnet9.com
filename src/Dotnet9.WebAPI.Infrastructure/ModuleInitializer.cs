namespace Dotnet9.WebAPI.Infrastructure;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IIdRepository, IdRepository>();
        services.AddScoped<IAboutRepository, AboutRepository>();
        services.AddScoped<IActionLogRepository, ActionLogRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<IDonationRepository, DonationRepository>();
    }
}