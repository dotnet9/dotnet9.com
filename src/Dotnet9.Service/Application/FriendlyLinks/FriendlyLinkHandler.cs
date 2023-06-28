using Google.Api;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Dotnet9.Service.Application.FriendlyLinks;

public class FriendlyLinkHandler
{
    private readonly IFriendlyLinkRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public FriendlyLinkHandler(IFriendlyLinkRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task AddAsync(FriendlyLinkCommand command, ISequentialGuidGenerator guidGenerator,
        CancellationToken cancellationToken)
    {
        var catalogItem = new FriendlyLink(guidGenerator.NewId(), command.Index, command.Name, command.Url,
            command.Description);
        await _repository.AddAsync(catalogItem, cancellationToken);
    }

    [EventHandler]
    public async Task GetListAsync(FriendlyLinksQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(FriendlyLinkHandler)}_{nameof(GetListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await _repository.GetFriendlyLinkListAsync(query);

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetFriendlyLinkListResponse>(dataFromDb, TimeSpan.FromMinutes(5))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetFriendlyLinkListResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = new PaginatedListBase<FriendlyLinkDto>()
            {
                Total = data.Total,
                TotalPages = data.TotalPage,
                Result = data.Records!
            };
        }
    }
}