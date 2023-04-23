using static Google.Rpc.Context.AttributeContext.Types;

namespace Dotnet9.Service.Infrastructure.Repositories;

public class AboutRepository : Repository<Dotnet9DbContext, About, Guid>, IAboutRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public AboutRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public async Task<About?> GetAsync()
    {
        async Task<About?> ReadDataFromDb()
        {
            return await Context.Set<About>().FirstOrDefaultAsync();
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(AboutRepository)}_{nameof(GetAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<About>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<About>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}