namespace Dotnet9.Service.Application.Albums;

public class AlbumHandler
{
    private readonly IAlbumRepository _repository;
    private readonly RedisClient _redisClient;

    public AlbumHandler(IAlbumRepository repository,
        RedisClient redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(AlbumsQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(AlbumHandler)}_{nameof(GetListAsync)}";
        var data = await _redisClient.GetAsync<List<AlbumBrief>>(key);
        if (data == null)
        {
            data = await _repository.GetAllBriefAsync();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

        if (data != null)
        {
            query.Result = new PaginatedListBase<AlbumBrief>()
            {
                Result = data
            };
        }
    }
}