namespace Dotnet9.Service.Services;

public class DonationService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public DonationService() : base("/api/donations")
    {
    }

    [RoutePattern(pattern: "/api/donations/")]
    public async Task<DonationDto?> GetAsync(CancellationToken cancellationToken)
    {
        var queryEvent = new DonationQuery();
        await EventBus.PublishAsync(queryEvent, cancellationToken);
        return queryEvent.Result;
    }
}