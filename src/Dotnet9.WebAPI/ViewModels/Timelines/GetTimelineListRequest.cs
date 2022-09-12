namespace Dotnet9.WebAPI.ViewModels.Timelines;

public record GetTimelineListRequest(string? Keywords, int PageIndex, int PageSize);