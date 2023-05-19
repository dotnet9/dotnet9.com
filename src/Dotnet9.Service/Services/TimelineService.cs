namespace Dotnet9.Service.Services;

public class TimelineService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public TimelineService() : base("/api/timelines")
    {
    }

    [RoutePattern(pattern: "/api/timelines")]
    public async Task<List<TimelineDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new TimelineQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}