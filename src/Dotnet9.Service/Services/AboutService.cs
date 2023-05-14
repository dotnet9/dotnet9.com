namespace Dotnet9.Service.Services;

public class AboutService : ServiceBase
{
    public AboutService() : base("/api/abouts")
    {
    }

    [RoutePattern(pattern: "/api/abouts/")]
    public async Task<AboutDto?> GetAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new AboutQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result;
    }
}