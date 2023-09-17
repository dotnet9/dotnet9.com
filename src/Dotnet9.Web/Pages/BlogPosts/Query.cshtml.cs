namespace Dotnet9.Web.Pages.BlogPosts;

public class QueryModel : PageModel
{
    private readonly IBlogPostService _blogPostService;
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly ILogger<QueryModel> _logger;

    public QueryModel(ILogger<QueryModel> logger, IBlogPostService blogPostService,
        IDistributedCacheHelper cacheHelper)
    {
        _logger = logger;
        _blogPostService = blogPostService;
        _cacheHelper = cacheHelper;
    }

    [BindProperty(SupportsGet = true)] public string? Keywords { get; set; }

    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 12;

    public int Total { get; set; }
    public int[]? Pages { get; set; }
    public int PageCount { get; set; }

    public List<BlogPostBriefForFront>? BlogPosts { get; set; }

    public async Task OnGet()
    {
        string cacheKey = $"QueryBlogPostBriefList_{Keywords}_{Current}_{PageSize}";

        async Task<GetBlogPostBriefListResponse?> GetBlogPostsFromDb()
        {
            GetBlogPostBriefListRequest request = new(Keywords, Current, PageSize);
            return await _blogPostService.BlogPostBriefListAsync(request);
        }

        GetBlogPostBriefListResponse? response = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetBlogPostsFromDb());

        BlogPosts = response!.Data;
        Total = response.Total;
        PageCount = response.Total.GetPageCount(PageSize);
        Pages = response.Total.GetPages(PageSize, Current, 10);
    }
}