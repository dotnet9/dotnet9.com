namespace Dotnet9.Service.Infrastructure.Repositories;

public class DonationRepository : Repository<Dotnet9DbContext, Donation, Guid>, IDonationRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public DonationRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public async Task<Donation?> GetAsync()
    {
        async Task<Donation?> ReadDataFromDb()
        {
            return await Context.Set<Donation>().FirstOrDefaultAsync();
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(DonationRepository)}_{nameof(GetAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<Donation>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<Donation>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}