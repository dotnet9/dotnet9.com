namespace Dotnet9.WebAPI.Domain.BlogPosts;

public interface IBlogPostRepository
{
    Task<(BlogPost[]? BlogPosts, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize);
    Task<(BlogPost[]? BlogPosts, long Count)> GetListByCategoryIdAsync(Guid categoryId, int pageIndex, int pageSize);
    Task<(BlogPost[]? BlogPosts, long Count)> GetListByAlbumIdAsync(Guid albumId, int pageIndex, int pageSize);
    Task<(BlogPost[]? BlogPosts, long Count)> GetListByTagIdAsync(Guid tagId, int pageIndex, int pageSize);
    Task<int> DeleteAsync(Guid[] ids);
    Task<BlogPost?> FindByIdAsync(Guid id);
    Task<BlogPost?> FindByTitleAsync(string name);
    Task<BlogPost?> FindBySlugAsync(string slug);
}