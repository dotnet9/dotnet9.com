namespace Dotnet9.WebAPI.ViewModel.Links;

public record GetLinkListRequest(string? Name,string? Url,string? Description,LinkKind? Kind, int Current, int PageSize);