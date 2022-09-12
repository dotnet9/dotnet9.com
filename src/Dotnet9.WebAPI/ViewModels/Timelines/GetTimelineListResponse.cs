namespace Dotnet9.WebAPI.ViewModels.Timelines;

public record GetTimelineListResponse(IEnumerable<TimelineDto>? Timelines, long TotalCount);