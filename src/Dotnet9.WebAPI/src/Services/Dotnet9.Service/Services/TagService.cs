namespace Dotnet9.Service.Services;

public class TagService : ServiceBase
{
    public TagService() : base("/api/tags")
    {
    }

    [RoutePattern(pattern: "/api/tags")]
    public async Task<List<TagBrief>> GetAllAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new TagQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}