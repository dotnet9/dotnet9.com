namespace Dotnet9.Service.Application.Timelines;

public class TimelineHandler
{
    private readonly ITimelineRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public TimelineHandler(ITimelineRepository repository,
        IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(TimelineQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(TimelineHandler)}_{nameof(GetListAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetListAsync());

        if (data != null)
        {
            query.Result = new PaginatedListBase<TimelineDto>()
            {
                Result = data
            };
        }
    }
}