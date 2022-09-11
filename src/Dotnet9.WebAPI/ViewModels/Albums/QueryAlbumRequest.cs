namespace Dotnet9.WebAPI.ViewModels.Albums;

public record QueryAlbumRequest(string? Keywords, int PageIndex, int PageSize);