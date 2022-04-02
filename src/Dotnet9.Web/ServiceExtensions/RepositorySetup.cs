using Dotnet9.Application.Albums;
using Dotnet9.Application.Blogs;
using Dotnet9.Application.Categories;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Application.Contracts.Tags;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Application.Tags;
using Dotnet9.Application.UrlLinks;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Tags;
using Dotnet9.Domain.UrlLinks;
using Dotnet9.EntityFrameworkCore.Albums;
using Dotnet9.EntityFrameworkCore.Blogs;
using Dotnet9.EntityFrameworkCore.Categories;
using Dotnet9.EntityFrameworkCore.Tags;
using Dotnet9.EntityFrameworkCore.UrlLinks;

namespace Dotnet9.Web.ServiceExtensions;

public static class RepositorySetup
{
    public static void AddRepositorySetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddTransient<IAlbumRepository, EfCoreAlbumRepository>();
        services.AddTransient(typeof(AlbumManager));
        services.AddTransient<IAlbumAppService, AlbumAppService>();

        services.AddTransient<ICategoryRepository, EfCoreCategoryRepository>();
        services.AddTransient(typeof(CategoryManager));
        services.AddTransient<ICategoryAppService, CategoryAppService>();

        services.AddTransient<ITagRepository, EfCoreTagRepository>();
        services.AddTransient(typeof(TagManager));
        services.AddTransient<ITagAppService, TagAppService>();

        services.AddTransient<IBlogPostRepository, EfCoreBlogPostRepository>();
        services.AddTransient(typeof(BlogPostManager));
        services.AddTransient<IBlogPostAppService, BlogPostAppService>();

        services.AddTransient<IUrlLinkRepository, EfCoreUrlLinkRepository>();
        services.AddTransient(typeof(UrlLinkManager));
        services.AddTransient<IUrlLinkAppService, UrlLinkAppService>();
    }
}