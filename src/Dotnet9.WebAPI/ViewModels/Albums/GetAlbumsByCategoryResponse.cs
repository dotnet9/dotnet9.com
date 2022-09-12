namespace Dotnet9.WebAPI.ViewModels.Albums;

public record GetAlbumsByCategoryResponse(IEnumerable<AlbumDto>? Albums, long TotalCount);