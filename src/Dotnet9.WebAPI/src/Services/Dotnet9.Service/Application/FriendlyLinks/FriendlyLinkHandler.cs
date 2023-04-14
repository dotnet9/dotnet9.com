using Dotnet9.Contracts.Dto.FriendlyLinks;

namespace Dotnet9.Service.Application.FriendlyLinks;

public class FriendlyLinkHandler
{
    private readonly IFriendlyLinkRepository _friendlyLinkRepository;

    public FriendlyLinkHandler(IFriendlyLinkRepository friendlyLinkRepository)
    {
        _friendlyLinkRepository = friendlyLinkRepository;
    }

    [EventHandler]
    public async Task AddAsync(FriendlyLinkCommand command, ISequentialGuidGenerator guidGenerator,
        CancellationToken cancellationToken)
    {
        var catalogItem = new FriendlyLink(guidGenerator.NewId(), command.Index, command.Name, command.Url,
            command.Description);
        await _friendlyLinkRepository.AddAsync(catalogItem, cancellationToken);
    }

    [EventHandler]
    public async Task GetListAsync(FriendlyLinksQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<FriendlyLink, bool>> condition = friendlyLink => true;
        condition = condition.And(!query.Name.IsNullOrWhiteSpace(),
            friendlyLink => friendlyLink.Name.Contains(query.Name!));

        var catalogItems = await _friendlyLinkRepository.GetPaginatedListAsync(condition,
            new PaginatedOptions(query.Page, query.PageSize), cancellationToken);

        query.Result = new PaginatedListBase<FriendlyLinkDto>()
        {
            Total = catalogItems.Total,
            TotalPages = catalogItems.TotalPages,
            Result = catalogItems.Result.Map<List<FriendlyLinkDto>>()
        };
    }
}