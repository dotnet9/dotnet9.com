namespace Dotnet9.WebAPI.ViewModels.Albums;

public record QueryAlbumResponse(IEnumerable<AlbumDTO>? Albums, long TotalCount);