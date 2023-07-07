namespace Dotnet9.Service.Application.Donations;

public class DonationHandler
{
    private readonly IDonationRepository _repository;
    private readonly RedisClient _redisClient;

    public DonationHandler(IDonationRepository repository,
        RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetAsync(DonationQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(DonationRepository)}_{nameof(GetAsync)}";

        var data = await _redisClient.GetAsync<DonationDto>(key);
        if (data == null)
        {
            data = (await _repository.GetAsync())?.Map<DonationDto>();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

        query.Result = data;
    }
}