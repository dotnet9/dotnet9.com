namespace Dotnet9.WebAPI.ViewModel.Albums;

public record GetAlbumListResponse(IEnumerable<AlbumDto>? Records, long Count);