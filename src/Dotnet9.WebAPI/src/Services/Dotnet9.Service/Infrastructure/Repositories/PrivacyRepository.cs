namespace Dotnet9.Service.Infrastructure.Repositories;

public class PrivacyRepository : Repository<Dotnet9DbContext, Privacy, Guid>, IPrivacyRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public PrivacyRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public async Task<Privacy?> GetAsync()
    {
        async Task<Privacy?> ReadDataFromDb()
        {
            return await Context.Set<Privacy>().FirstOrDefaultAsync();
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(PrivacyRepository)}_{nameof(GetAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<Privacy>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<Privacy>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}