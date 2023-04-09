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
        var catalogInfo = await _multilevelCacheClient.GetOrSetAsync(id.ToString(), () =>
        {
            var info = Context.Set<FriendlyLink>()
                .AsSplitQuery()
                .FirstOrDefaultAsync(friendlyLink => friendlyLink.Id == id, cancellationToken).ConfigureAwait(false)
                .GetAwaiter().GetResult();

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

    public async Task<FriendlyLink?> FindByIdAsync(Guid id)
    {
        return await Context.FriendlyLinks!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<FriendlyLink?> FindByNameAsync(string name)
    {
        return await Context.FriendlyLinks!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<FriendlyLink?> FindByUrlAsync(string url)
    {
        return await Context.FriendlyLinks!.FirstOrDefaultAsync(x => x.Url == url);
    }
}