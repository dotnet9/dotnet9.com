namespace Dotnet9.Application.Contracts.Blogs;

public interface IBlogPostAppService
{
    Task<BlogPostWithDetailsDto?> FindBySlugAsync(string slug);
    Task<List<BlogPostForSitemap>> GetListBlogPostForSitemap();
}