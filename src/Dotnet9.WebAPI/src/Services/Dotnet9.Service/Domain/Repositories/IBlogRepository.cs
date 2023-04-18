namespace Dotnet9.Service.Domain.Repositories;

public interface IBlogRepository : IRepository<Blog, Guid>
{
    Task<Blog?> FindByIdAsync(Guid id);
    Task<Blog?> FindByTitleAsync(string name);
    Task<Blog?> FindBySlugAsync(string slug);
    Task<List<BlogBrief>> GetBlogBriefListAsync();
    Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(SearchBlogsByAlbumQuery query);
}