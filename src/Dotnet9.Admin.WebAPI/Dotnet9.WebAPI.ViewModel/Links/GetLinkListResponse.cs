namespace Dotnet9.WebAPI.ViewModel.Links;

public record GetLinkListResponse(IEnumerable<LinkDto>? Data, long Total, bool Success, int PageSize, int Current);