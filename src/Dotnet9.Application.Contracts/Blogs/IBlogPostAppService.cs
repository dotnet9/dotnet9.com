namespace Dotnet9.Application.Contracts.Blogs;

public interface IBlogPostAppService
{
    Task<BlogPostViewModel?> FindBySlugAsync(string slug);
    Task<BlogPostWithDetailsDto?> GetByIdAsync(int id);
    Task<RecommendViewModel> GetRecommendBlogPostAsync();
    Task<List<BlogPostForSitemap>> GetListBlogPostForSitemapAsync();
    Task<PageDto<BlogPostDto>> AdminListAsync(BlogPostRequest request);
}