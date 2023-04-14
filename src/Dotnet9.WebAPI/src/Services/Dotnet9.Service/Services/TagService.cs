namespace Dotnet9.Service.Services;

public class TagService : ServiceBase
{
    public TagService() : base("/api/tag")
    {
    }

    public async Task<List<TagBrief>> GetHotAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new TagQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}