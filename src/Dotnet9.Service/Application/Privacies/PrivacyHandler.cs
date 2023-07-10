using Google.Api;

namespace Dotnet9.Service.Application.Privacies;

public class PrivacyHandler
{
    private readonly IPrivacyRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public PrivacyHandler(IPrivacyRepository repository,
        IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetAsync(PrivacyQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(PrivacyRepository)}_{nameof(GetAsync)}";

        var data = await _redisClient.GetOrCreateAsync(key, async(e)=>(await _repository.GetAsync())?.Map<PrivacyDto>());

        query.Result = data;
    }
}