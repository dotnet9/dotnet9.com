namespace Dotnet9.Service.Application.Abouts;

public class AboutHandler
{
    private readonly IAboutRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public AboutHandler(IAboutRepository repository,
        IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetAsync(AboutQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(AboutHandler)}_{nameof(GetAsync)}";
        var data =await _redisClient.GetOrCreateAsync(key, async(e)=> (await _repository.GetAsync())?.Map<AboutDto?>());
        
        query.Result = data;
    }
}