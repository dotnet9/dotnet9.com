namespace Dotnet9.Application.Contracts.Albums;

public interface IAlbumAppService
{
    Task<List<AlbumCountDto>> GetListCountAsync();
}