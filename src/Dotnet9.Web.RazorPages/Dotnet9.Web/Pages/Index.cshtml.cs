namespace Dotnet9.Web.Pages;

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

    public List<BlogPostBrief>? BlogPosts { get; set; }

    [BindProperty(SupportsGet = true)] public string? Keywords { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public int Total { get; set; }
    public int[]? Pages { get; set; }
    public int PageCount { get; set; }


    public async Task OnGet()
    {
        string cacheKey = $"BlogPostBriefList_{Keywords}_{Current}_{PageSize}";

        async Task<GetBlogPostBriefListResponse?> GetBlogPostsFromDb()
        {
            GetBlogPostBriefListRequest request = new GetBlogPostBriefListRequest(Keywords, Current, PageSize);
            return await _blogPostService.GetBlogPostBriefListAsync(request);
        }

        GetBlogPostBriefListResponse? response = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetBlogPostsFromDb());


        BlogPosts = response!.Data;
        Total = response.Total;
        PageCount = response.Total.GetPageCount(PageSize);
        Pages = response.Total.GetPages(PageSize, Current, 10);
    }
}