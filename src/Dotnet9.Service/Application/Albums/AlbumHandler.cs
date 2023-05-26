namespace Dotnet9.Service.Application.Albums;

public class AlbumHandler
{
    private readonly IAlbumRepository _repository;
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public AlbumHandler(IAlbumRepository repository,
        IMultilevelCacheClient multilevelCacheClient)
    {
        _repository = repository;
        _multilevelCacheClient = multilevelCacheClient;
    }

    [EventHandler]
    public async Task GetListAsync(AlbumsQuery query, CancellationToken cancellationToken)
    {
        TimeSpan? timeSpan = null;
        var key = $"{nameof(AlbumHandler)}_{nameof(GetListAsync)}";
        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var distinctAlbumList = await _repository.GetAllBriefAsync();

            if (distinctAlbumList.Any())
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<AlbumBrief>>(distinctAlbumList, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<AlbumBrief>>(distinctAlbumList);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        query.Result = new PaginatedListBase<AlbumBrief>()
        {
            Result = data
        };
    }
}