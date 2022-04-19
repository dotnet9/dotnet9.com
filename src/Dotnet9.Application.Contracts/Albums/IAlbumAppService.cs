namespace Dotnet9.Application.Contracts.Albums;

public interface IAlbumAppService
{
    Task<AlbumViewModel?> GetAlbumAsync(string? slug);

    Task<List<AlbumCountDto>> GetListCountAsync();
}