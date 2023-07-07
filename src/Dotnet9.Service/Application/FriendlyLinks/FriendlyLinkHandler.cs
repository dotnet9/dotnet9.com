using Google.Api;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Dotnet9.Service.Application.FriendlyLinks;

public class FriendlyLinkHandler
{
    private readonly IFriendlyLinkRepository _repository;
    private readonly RedisClient _redisClient;

    public FriendlyLinkHandler(IFriendlyLinkRepository repository,
        RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
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
        const string key = $"{nameof(FriendlyLinkHandler)}_{nameof(GetListAsync)}";

        var data = await _redisClient.GetAsync<GetFriendlyLinkListResponse>(key);
        if (data == null)
        {
            data = await _repository.GetFriendlyLinkListAsync(query);
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

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