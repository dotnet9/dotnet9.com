namespace Dotnet9.WebAPI.ViewModel.Timelines;

public record GetTimelineListResponse(IEnumerable<TimelineDto>? Timelines, long Total);