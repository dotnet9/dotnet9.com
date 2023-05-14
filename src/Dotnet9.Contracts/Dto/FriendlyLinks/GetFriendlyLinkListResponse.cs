namespace Dotnet9.Contracts.Dto.FriendlyLinks;

public record GetFriendlyLinkListResponse(bool Success, List<FriendlyLinkDto>? Records = null,
    long Total = 0,
    int TotalPage = 0,
    int PageSize = 20,
    int Page = 1);