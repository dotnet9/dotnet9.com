namespace Dotnet9.Service.Application.Tags;

public class TagHandler
{
    private readonly ITagRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public TagHandler(ITagRepository repository, IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(TagQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(TagHandler)}_{nameof(GetListAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>await _repository.GetTagBriefListAsync());

        if (data != null)
        {
            query.Result = new PaginatedListBase<TagBrief>()
            {
                Result = data
            };
        }
    }
}