namespace Dotnet9.Service.Application.Donations;

public class DonationHandler
{
    private readonly IDonationRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public DonationHandler(IDonationRepository repository,
        IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetAsync(DonationQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(DonationRepository)}_{nameof(GetAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>(await _repository.GetAsync())?.Map<DonationDto>());

        query.Result = data;
    }
}