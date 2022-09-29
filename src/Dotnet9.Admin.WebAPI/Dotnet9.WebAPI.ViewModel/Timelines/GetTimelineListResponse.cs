namespace Dotnet9.WebAPI.ViewModel.Timelines;

public record GetTimelineListResponse(IEnumerable<TimelineDto>? Data, long Total, bool Success, int PageSize, int Current);