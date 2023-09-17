namespace Dotnet9.Web.Service;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ILinkService, LinkService>();
        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<ITagService, TagService>();
    }
}