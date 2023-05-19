namespace Dotnet9.Service.Services;

public class AboutService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public AboutService() : base("/api/abouts")
    {
    }

    [RoutePattern(pattern: "/api/abouts/")]
    public async Task<AboutDto?> GetAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new AboutQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result;
    }
}