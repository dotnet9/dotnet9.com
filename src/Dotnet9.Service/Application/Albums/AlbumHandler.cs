namespace Dotnet9.Service.Application.Albums;

public class AlbumHandler
{
    private readonly IAlbumRepository _repository;
    private readonly IDistributedCacheHelper _redisClient;

    public AlbumHandler(IAlbumRepository repository,
        IDistributedCacheHelper redisClient)
    {
        _repository = repository;
        _redisClient = redisClient;
    }

    [EventHandler]
    public async Task GetListAsync(AlbumsQuery query, CancellationToken cancellationToken)
    {
        const string key = $"{nameof(AlbumHandler)}_{nameof(GetListAsync)}";
        var data = await _redisClient.GetOrCreateAsync(key, async (e) => await _repository.GetAllBriefAsync());

        if (data != null)
        {
            query.Result = new PaginatedListBase<AlbumBrief>()
            {
                Result = data
            };
        }
    }
}