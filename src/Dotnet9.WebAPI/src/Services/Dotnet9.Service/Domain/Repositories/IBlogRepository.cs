namespace Dotnet9.Service.Domain.Repositories;

public interface IBlogRepository : IRepository<Blog, Guid>
{
    Task<Blog?> FindByIdAsync(Guid id);
    Task<Blog?> FindByTitleAsync(string name);
    Task<BlogDetails?> FindBySlugAsync(string slug);
    Task<List<BlogBrief>> GetBlogBriefListAsync();
    Task<GetBlogListByKeywordsResponse> GetBlogBriefListByKeywordsAsync(SearchBlogsByKeywordsQuery query);
    Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(SearchBlogsByAlbumQuery query);
    Task<GetBlogListByCategorySlugResponse> GetBlogBriefListByCategorySlugAsync(SearchBlogsByCategoryQuery query);
}