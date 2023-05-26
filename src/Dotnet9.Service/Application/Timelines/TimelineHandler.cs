namespace Dotnet9.Service.Application.Timelines;

public class TimelineHandler
{
    private readonly ITimelineRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public TimelineHandler(ITimelineRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetListAsync(TimelineQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(TimelineHandler)}_{nameof(GetListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetListAsync().Result;

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<TimelineDto>?>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<TimelineDto>?>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = new PaginatedListBase<TimelineDto>()
            {
                Result = data
            };
        }
    }
}