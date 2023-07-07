namespace Dotnet9.Service.Application.Abouts;

public class AboutHandler
{
    private readonly IAboutRepository _repository;
    private readonly RedisClient _redisClient;

    public AboutHandler(IAboutRepository repository,
        RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetAsync(AboutQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(AboutHandler)}_{nameof(GetAsync)}";
        var data =await _redisClient.GetAsync<AboutDto>(key);
        if (data == null)
        {
            data = (await _repository.GetAsync())?.Map<AboutDto?>();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }
        
        query.Result = data;
    }
}