namespace Dotnet9.Service.Domain.Repositories;

public interface IAlbumRepository : IRepository<Album, Guid>
{
    Task<Album?> FindByIdAsync(Guid id);
    Task<Album?> FindByNameAsync(string name);
    Task<Album?> FindBySlugAsync(string slug);
    Task<List<AlbumBrief>?> GetAllBriefAsync();
}
