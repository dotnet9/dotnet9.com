namespace Dotnet9.Service.Services;

public class SystemService : ServiceBase
{
    private readonly Dotnet9DbContext _dataContext;
    private readonly IMultilevelCacheClient _multilevelCacheClient;
    private IOptions<SiteOptions> Options { get; }

    public SystemService(IOptions<SiteOptions> options, Dotnet9DbContext dataContext,
        IMultilevelCacheClient multilevelCacheClient) : base("/api/systems")
    {
        _dataContext = dataContext;
        _multilevelCacheClient = multilevelCacheClient;
        Options = options;
    }

    public async Task<SiteInfo> GetAsync()
    {
        return await Task.FromResult(Options.Value.Map<SiteInfo>());
    }

    public async Task<SitemapInfo?> GetSitemapAsync()
    {
        async Task<SitemapInfo> ReadDataFromDb()
        {
            var albums = await _dataContext.Set<Album>().Select(album => album.Slug).ToListAsync();
            var categories = await _dataContext.Set<Category>().Select(category => category.Slug).ToListAsync();
            var blogs = await _dataContext.Set<Blog>().Select(blog => new { blog.CreationTime, blog.Slug })
                .ToDictionaryAsync(blog => blog.Slug, blog => blog.CreationTime);
            return new SitemapInfo(albums.ToArray(), categories.ToArray(), blogs);
        }


        TimeSpan? timeSpan = null;
        var key = $"{nameof(SystemService)}_{nameof(GetSitemapAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            timeSpan = TimeSpan.FromSeconds(30);
            return new CacheEntry<SitemapInfo>(dataFromDb, TimeSpan.FromDays(3))
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}