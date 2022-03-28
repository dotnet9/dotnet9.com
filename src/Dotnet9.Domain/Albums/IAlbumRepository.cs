using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Albums;

public interface IAlbumRepository : IRepository<Album>
{
    Task<Album?> FindByNameAsync(string name);
    Task<Album?> FindBySlugAsync(string slug);
    Task<List<AlbumCount>> GetListCountAsync();
}