namespace Dotnet9.Service.Domain.Repositories;

public interface IBlogRepository : IRepository<Blog, Guid>
{
    Task CreateBlogViewCount(string slug, string ip, DateTime creationTime);
    Task CreateBlogSearchCount(string keywords, string ip, DateTime creationTime);
    Task<Blog?> FindByIdAsync(Guid id);
    Task<Blog?> FindByTitleAsync(string name);
    Task<Blog?> FindBySlugAsync(string slug);
    Task<BlogDetails?> FindDetailsBySlugAsync(string slug);
    Task<List<BlogBrief>?> GetBlogBriefListOfRecommendAsync();
    Task<List<BlogBrief>?> GetBlogBriefListOfWeekHotAsync();
    Task<List<BlogBrief>?> GetBlogBriefListOfHistoryHotAsync();
    Task<List<BlogArchive>?> GetBlogArchiveListAsync();
    Task<GetBlogListByKeywordsResponse?> GetBlogBriefListByKeywordsAsync(SearchBlogsByKeywordsQuery query);
    Task<GetBlogListByAlbumSlugResponse?> GetBlogBriefListByAlbumSlugAsync(SearchBlogsByAlbumQuery query);
    Task<GetBlogListByCategorySlugResponse?> GetBlogBriefListByCategorySlugAsync(SearchBlogsByCategoryQuery query);
    Task<GetBlogListByTagNameResponse?> GetBlogBriefListByTagNameAsync(SearchBlogsByTagQuery query);
    Task<List<BlogSearchCountDto>?> GetTopSearchKeywordsAsync();
}