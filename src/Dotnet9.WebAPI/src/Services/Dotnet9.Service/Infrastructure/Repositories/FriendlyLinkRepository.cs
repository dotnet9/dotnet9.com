namespace Dotnet9.Service.Infrastructure.Repositories;

public class FriendlyLinkRepository : Repository<Dotnet9DbContext, FriendlyLink, Guid>, IFriendlyLinkRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public FriendlyLinkRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public override async Task<FriendlyLink?> FindAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        TimeSpan? timeSpan = null;
        var catalogInfo = await _multilevelCacheClient.GetOrSetAsync(id.ToString(), async () =>
        {
            var info = await Context.Set<FriendlyLink>()
                .AsSplitQuery()
                .FirstOrDefaultAsync(friendlyLink => friendlyLink.Id == id, cancellationToken);

            if (info != null)
            {
                return new CacheEntry<FriendlyLink>(info, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<FriendlyLink>(info);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return catalogInfo;
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

    public async Task<GetFriendlyLinkListResponse> GetFriendlyLinkListAsync(FriendlyLinksQuery request)
    {
        async Task<GetFriendlyLinkListResponse?> ReadDataFromDb()
        {
            var query = Context.Set<FriendlyLink>().AsQueryable();
            var total = await query.CountAsync();
            var dataListFromDb = await query.OrderBy(friendlyLink => friendlyLink.Index).ToListAsync();
            if (dataListFromDb.Any())
            {
                var totalPage = (total + request.PageSize - 1) / request.PageSize;
                return new GetFriendlyLinkListResponse(true, dataListFromDb.Map<List<FriendlyLinkDto>>(), total,
                    totalPage
                    , request.PageSize, request.Page);
            }

            return null;
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(FriendlyLinkRepository)}_{nameof(GetFriendlyLinkListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetFriendlyLinkListResponse>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetFriendlyLinkListResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}