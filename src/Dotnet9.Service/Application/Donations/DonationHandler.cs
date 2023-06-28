namespace Dotnet9.Service.Application.Donations;

public class DonationHandler
{
    private readonly IDonationRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public DonationHandler(IDonationRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetAsync(DonationQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        const string key = $"{nameof(DonationRepository)}_{nameof(GetAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = _repository.GetAsync().Result?.Map<DonationDto?>();

            if (dataFromDb != null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<DonationDto>(dataFromDb, TimeSpan.FromMinutes(5))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<DonationDto>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        query.Result = data;
    }
}