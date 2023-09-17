namespace Dotnet9.Web.Service.Albums;

public interface IAlbumService
{
    Task<List<AlbumBrief>> GetAlbumsAsync();
}