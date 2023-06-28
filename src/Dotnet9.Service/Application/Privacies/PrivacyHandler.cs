using Google.Api;

namespace Dotnet9.Service.Application.Privacies;

public class PrivacyHandler
{
    private readonly IPrivacyRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public PrivacyHandler(IPrivacyRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetAsync(PrivacyQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(PrivacyRepository)}_{nameof(GetAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetAsync().Result?.Map<PrivacyDto?>();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<PrivacyDto?>(dataFromDb, TimeSpan.FromMinutes(5))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<PrivacyDto?>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        query.Result = data;
    }
}