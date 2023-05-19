namespace Dotnet9.Service.Services;

public class FriendlyLinkService : ServiceBase
{
    private IEventBus EventBus => GetRequiredService<IEventBus>();

    public FriendlyLinkService() : base("/api/links")
    {
    }

    public async Task<PaginatedListBase<FriendlyLinkDto>> GetAsync(CancellationToken cancellationToken, string? name,
        int page = 1, int pageSize = 20)
    {
        var friendlyLinkQueryEvent = new FriendlyLinksQuery { Name = name, Page = page, PageSize = pageSize };
        await EventBus.PublishAsync(friendlyLinkQueryEvent, cancellationToken);
        return friendlyLinkQueryEvent.Result;
    }
}