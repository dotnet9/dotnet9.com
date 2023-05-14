namespace Dotnet9.Service.Services;

public class TimelineService : ServiceBase
{
    public TimelineService() : base("/api/timelines")
    {
    }

    [RoutePattern(pattern: "/api/timelines")]
    public async Task<List<TimelineDto>> GetAllAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new TimelineQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}