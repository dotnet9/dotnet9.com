namespace Dotnet9.WebAPI.ViewModels.Albums;

public record GetAlbumListResponse(IEnumerable<AlbumDto>? Albums, long TotalCount);