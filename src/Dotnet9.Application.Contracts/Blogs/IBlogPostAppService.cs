namespace Dotnet9.Application.Contracts.Blogs;

public interface IBlogPostAppService
{
    Task<BlogPostViewModel?> FindBySlugAsync(string slug);
    Task<List<BlogPostForSitemap>> GetListBlogPostForSitemap();
}