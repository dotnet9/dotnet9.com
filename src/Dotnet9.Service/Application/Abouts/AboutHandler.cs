namespace Dotnet9.Service.Application.Abouts;

public class AboutHandler
{
    private readonly IAboutRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public AboutHandler(IAboutRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetAsync(AboutQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(AboutHandler)}_{nameof(GetAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = (await _repository.GetAsync())?.Map<AboutDto?>();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<AboutDto?>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<AboutDto?>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);
        
        query.Result = data;
    }
}