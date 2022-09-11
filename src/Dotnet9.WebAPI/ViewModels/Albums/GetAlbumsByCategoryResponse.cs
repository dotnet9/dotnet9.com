namespace Dotnet9.WebAPI.ViewModels.Albums;

public record GetAlbumsByCategoryResponse(IEnumerable<AlbumDTO>? Albums, long TotalCount);