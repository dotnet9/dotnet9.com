namespace Dotnet9.Service.Infrastructure.Repositories;

public class FriendlyLinkRepository : Repository<Dotnet9DbContext, FriendlyLink, Guid>, IFriendlyLinkRepository
{
    public FriendlyLinkRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public override Task<FriendlyLink?> FindAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return Context.Set<FriendlyLink>()
            .AsSplitQuery()
            .FirstOrDefaultAsync(friendlyLink => friendlyLink.Id == id, cancellationToken);
    }

    public Task<FriendlyLink?> FindByIdAsync(Guid id)
    {
        return Context.FriendlyLinks!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<FriendlyLink?> FindByNameAsync(string name)
    {
        return Context.FriendlyLinks!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public Task<FriendlyLink?> FindByUrlAsync(string url)
    {
        return Context.FriendlyLinks!.FirstOrDefaultAsync(x => x.Url == url);
    }

    public async Task<GetFriendlyLinkListResponse?> GetFriendlyLinkListAsync(FriendlyLinksQuery request)
    {
        var query = Context.Set<FriendlyLink>().AsQueryable();
        var total = await query.CountAsync();
        var dataListFromDb = await query.OrderBy(friendlyLink => friendlyLink.Index).ToListAsync();
        if (dataListFromDb.Any() != true)
        {
            return null;
        }

        var totalPage = (total + request.PageSize - 1) / request.PageSize;
        return new GetFriendlyLinkListResponse(true, dataListFromDb.Map<List<FriendlyLinkDto>>(), total,
            totalPage
            , request.PageSize, request.Page);
    }
}