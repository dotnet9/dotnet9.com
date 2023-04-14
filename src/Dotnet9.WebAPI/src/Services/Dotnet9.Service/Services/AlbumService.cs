namespace Dotnet9.Service.Services;

public class AlbumService : ServiceBase
{
    public AlbumService() : base("/api/album")
    {
    }

    public async Task<List<AlbumBrief>> GetBriefAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new AlbumsQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}