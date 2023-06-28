namespace Dotnet9.Service.Application.Tags;

public class TagHandler
{
    private readonly ITagRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public TagHandler(ITagRepository repository, IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetListAsync(TagQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(TagHandler)}_{nameof(GetListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetTagBriefListAsync().Result;

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<TagBrief>>(dataFromDb, TimeSpan.FromMinutes(5))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<TagBrief>>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        if (data != null)
        {
            query.Result = new PaginatedListBase<TagBrief>()
            {
                Result = data
            };
        }
    }
}