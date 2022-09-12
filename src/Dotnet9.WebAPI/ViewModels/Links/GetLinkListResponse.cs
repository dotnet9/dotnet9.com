namespace Dotnet9.WebAPI.ViewModels.Links;

public record GetLinkListResponse(IEnumerable<LinkDto>? Links, long TotalCount);