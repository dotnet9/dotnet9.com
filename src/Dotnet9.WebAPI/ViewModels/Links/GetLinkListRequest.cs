namespace Dotnet9.WebAPI.ViewModels.Links;

public record GetLinkListRequest(string? Keywords, int PageIndex, int PageSize);