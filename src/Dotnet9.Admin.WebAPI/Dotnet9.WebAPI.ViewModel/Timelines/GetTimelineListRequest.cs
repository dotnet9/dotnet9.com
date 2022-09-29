namespace Dotnet9.WebAPI.ViewModel.Timelines;

public record GetTimelineListRequest(string? Keywords, int Current, int PageSize);