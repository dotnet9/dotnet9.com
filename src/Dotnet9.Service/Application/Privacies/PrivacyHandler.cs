using Google.Api;

namespace Dotnet9.Service.Application.Privacies;

public class PrivacyHandler
{
    private readonly IPrivacyRepository _repository;
    private readonly RedisClient _redisClient;

    public PrivacyHandler(IPrivacyRepository repository,
        RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetAsync(PrivacyQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(PrivacyRepository)}_{nameof(GetAsync)}";

        var data = await _redisClient.GetAsync<PrivacyDto>(key);
        if (data == null)
        {
            data = (await _repository.GetAsync())?.Map<PrivacyDto>();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

        query.Result = data;
    }
}