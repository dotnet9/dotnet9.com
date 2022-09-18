namespace Dotnet9.WebAPI.ViewModel.Albums;

public record GetAlbumListResponse(IEnumerable<AlbumDto>? Albums, long Total);