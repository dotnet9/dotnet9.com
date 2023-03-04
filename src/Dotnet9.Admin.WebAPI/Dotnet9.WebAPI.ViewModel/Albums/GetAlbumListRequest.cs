namespace Dotnet9.WebAPI.ViewModel.Albums;

public record GetAlbumListRequest(string? Keywords = null, int Current = 1, int PageSize = 10);