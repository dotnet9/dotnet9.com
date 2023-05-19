namespace Dotnet9.Service.Services;

public class TagService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public TagService() : base("/api/tags")
    {
    }

    [RoutePattern(pattern: "/api/tags")]
    public async Task<List<TagBrief>> GetAllAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new TagQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}