namespace Dotnet9.Service.Services;

public class PrivacyService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public PrivacyService() : base("/api/privacies")
    {
    }

    [RoutePattern(pattern: "/api/privacies/")]
    public async Task<PrivacyDto?> GetAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new PrivacyQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result;
    }
}