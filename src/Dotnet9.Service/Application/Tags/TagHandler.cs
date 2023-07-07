namespace Dotnet9.Service.Application.Tags;

public class TagHandler
{
    private readonly ITagRepository _repository;
    private readonly RedisClient _redisClient;

    public TagHandler(ITagRepository repository, RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(TagQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(TagHandler)}_{nameof(GetListAsync)}";

        var data = await _redisClient.GetAsync<List<TagBrief>>(key);
        if (data == null)
        {
            data = await _repository.GetTagBriefListAsync();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

        if (data != null)
        {
            query.Result = new PaginatedListBase<TagBrief>()
            {
                Result = data
            };
        }
    }
}