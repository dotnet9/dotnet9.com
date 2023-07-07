namespace Dotnet9.Service.Application.Timelines;

public class TimelineHandler
{
    private readonly ITimelineRepository _repository;
    private readonly RedisClient _redisClient;

    public TimelineHandler(ITimelineRepository repository,
        RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(TimelineQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(TimelineHandler)}_{nameof(GetListAsync)}";

        var data = await _redisClient.GetAsync<List<TimelineDto>>(key);
        if (data == null)
        {
            data = await _repository.GetListAsync();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

        if (data != null)
        {
            query.Result = new PaginatedListBase<TimelineDto>()
            {
                Result = data
            };
        }
    }
}