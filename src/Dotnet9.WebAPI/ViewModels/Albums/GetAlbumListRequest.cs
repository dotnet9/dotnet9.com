namespace Dotnet9.WebAPI.ViewModels.Albums;

public record GetAlbumListRequest(string? Keywords, int PageIndex, int PageSize);