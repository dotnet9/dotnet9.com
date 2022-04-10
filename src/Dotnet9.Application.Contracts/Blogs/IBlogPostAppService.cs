namespace Dotnet9.Application.Contracts.Blogs;

public interface IBlogPostAppService
{
    Task<BlogPostViewModel?> FindBySlugAsync(string slug);
    Task<RecommendViewModel> GetRecommendBlogPostAsync();
    Task<List<BlogPostForSitemap>> GetListBlogPostForSitemapAsync();

    Task<bool> AddOrUpdateQueryCountAsync(QueryCountForCreationOrUpdateDto queryCountForCreationOrUpdate);

    Task<bool> AddOrUpdateViewCountAsync(ViewCountForCreationOrUpdateDto viewCountForCreationOrUpdate);
}