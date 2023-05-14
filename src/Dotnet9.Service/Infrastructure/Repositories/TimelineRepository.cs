namespace Dotnet9.Service.Infrastructure.Repositories;

public class TimelineRepository : Repository<Dotnet9DbContext, Timeline, Guid>, ITimelineRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public TimelineRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public async Task<List<TimelineDto>?> GetListAsync()
    {
        async Task<List<TimelineDto>?> ReadDataFromDb()
        {
            return (await Context.Set<Timeline>().ToListAsync()).Adapt<List<TimelineDto>>();
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(TimelineRepository)}_{nameof(GetListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<TimelineDto>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<TimelineDto>>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}