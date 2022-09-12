namespace Dotnet9.WebAPI.Domain.Albums;

public interface IAlbumRepository
{
    Task<(Album[]? Albums, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize);
    Task<Category[]> GetCategoriesOfAlbumAsync();
    Task<(Album[]? Albums, long Count)> GetAlbumsByCategoryAsync(Guid categoryId, int pageIndex, int pageSize);
    Task<int> DeleteAsync(Guid[] ids);
    Task<Album?> FindByIdAsync(Guid id);
    Task<Album?> FindByNameAsync(string name);
    Task<Album?> FindBySlugAsync(string slug);
}