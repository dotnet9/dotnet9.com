namespace Dotnet9.Service.Services;

public class AlbumService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public AlbumService() : base("/api/albums")
    {
    }

    public async Task<List<AlbumBrief>> GetBriefAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new AlbumsQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result.Result;
    }
}