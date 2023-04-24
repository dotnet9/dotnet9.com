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
        var getLinkResult = await _repository.GetFriendlyLinkListAsync(query);
        query.Result = new PaginatedListBase<FriendlyLinkDto>()
        {
            Total = getLinkResult.Total,
            TotalPages = getLinkResult.TotalPage,
            Result = getLinkResult.Records
        };
    }
}