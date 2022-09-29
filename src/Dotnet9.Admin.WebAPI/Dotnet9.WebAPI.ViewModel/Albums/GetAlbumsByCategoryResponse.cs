namespace Dotnet9.WebAPI.ViewModel.Albums;

public record GetAlbumsByCategoryResponse(IEnumerable<AlbumDto>? Albums, long TotalCount);