namespace Dotnet9.Service.Services;

public class PrivacyService : ServiceBase
{
    public PrivacyService() : base("/api/privacies")
    {
    }

    [RoutePattern(pattern: "/api/privacies/")]
    public async Task<PrivacyDto?> GetAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new PrivacyQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result;
    }
}