namespace Dotnet9.Service.Services;

public class SystemService : ServiceBase
{
    private readonly Dotnet9DbContext _dataContext;
    private readonly RedisClient _redisClient;
    private IOptions<SiteOptions> Options { get; }

    public SystemService(IOptions<SiteOptions> options, Dotnet9DbContext dataContext,
        RedisClient redisClient) : base("/api/systems")
    {
        _dataContext = dataContext;
        _redisClient = redisClient;
        Options = options;
    }

    public async Task<SiteInfoDto> GetSiteAsync()
    {
        return await Task.FromResult(Options.Value.Map<SiteInfoDto>());
    }

    public async Task<SitemapInfo?> GetSitemapAsync()
    {
        async Task<SitemapInfo?> ReadDataFromDb()
        {
            var albums = await _dataContext.Set<Album>().Select(album => album.Slug).ToListAsync();
            var categories = await _dataContext.Set<Category>().Select(category => category.Slug).ToListAsync();
            var blogs = await _dataContext.Set<Blog>().Select(blog => new { blog.CreationTime, blog.Slug })
                .ToDictionaryAsync(blog => blog.Slug, blog => blog.CreationTime);
            return new SitemapInfo(albums.ToArray(), categories.ToArray(), blogs);
        }

        const string key = $"{nameof(SystemService)}_{nameof(GetSitemapAsync)}";

        var data = await _redisClient.GetAsync<SitemapInfo>(key);
        if (data == null)
        {
            data = await ReadDataFromDb();
            if (data != null)
            {
                await _redisClient.SetAsync(key, data, 300);
            }
        }

        return data;
    }
}