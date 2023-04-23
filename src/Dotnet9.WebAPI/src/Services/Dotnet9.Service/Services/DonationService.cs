namespace Dotnet9.Service.Services;

public class DonationService : ServiceBase
{
    public DonationService() : base("/api/donations")
    {
    }

    [RoutePattern(pattern: "/api/donations/")]
    public async Task<DonationDto?> GetAsync(IEventBus eventBus,
        CancellationToken cancellationToken)
    {
        var queryEvent = new DonationQuery();
        await eventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result;
    }
}