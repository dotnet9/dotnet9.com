namespace Dotnet9.Web.Pages.Albums;

public class IndexModel : PageModel
{
    private readonly IBlogPostService _blogPostService;
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IBlogPostService blogPostService, IDistributedCacheHelper cacheHelper)
    {
        _logger = logger;
        _blogPostService = blogPostService;
        _cacheHelper = cacheHelper;
    }

    public string? AlbumName { get; set; }

    public List<BlogPostBriefForFront>? BlogPosts { get; set; }

    [BindProperty(SupportsGet = true)] public string Slug { get; set; } = null!;

    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public int Total { get; set; }
    public int[]? Pages { get; set; }
    public int PageCount { get; set; }


    public async Task OnGet()
    {
        string cacheKey = $"AlbumBlogPostBriefList_{Slug}_{Current}_{PageSize}";

        async Task<GetBlogPostBriefListByAlbumSlugResponse?> GetBlogPostsFromDb()
        {
            GetBlogPostBriefListByAlbumSlugRequest request = new(Slug, Current, PageSize);
            return await _blogPostService.GetBlogPostBriefListByAlbumSlugAsync(request);
        }

        GetBlogPostBriefListByAlbumSlugResponse? response = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetBlogPostsFromDb());

        AlbumName = response!.AlbumName;
        BlogPosts = response.Data;
        Total = response.Total;
        PageCount = response.Total.GetPageCount(PageSize);
        Pages = response.Total.GetPages(PageSize, Current, 10);
    }
}