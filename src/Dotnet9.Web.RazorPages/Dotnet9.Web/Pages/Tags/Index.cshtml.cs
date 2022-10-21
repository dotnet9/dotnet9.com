namespace Dotnet9.Web.Pages.Tags;

public class IndexModel : PageModel
{
    private readonly IBlogPostService _blogPostService;
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly ILogger<IndexModel> _logger;
    private readonly ITagService _service;

    public IndexModel(ILogger<IndexModel> logger, ITagService service, IBlogPostService blogPostService,
        IDistributedCacheHelper cacheHelper)
    {
        _logger = logger;
        _service = service;
        _blogPostService = blogPostService;
        _cacheHelper = cacheHelper;
    }

    public string? TagName { get; set; }

    public List<TagBrief>? Tags { get; set; }
    public List<BlogPostBriefForFront>? BlogPosts { get; set; }

    [BindProperty(SupportsGet = true)] public string? Name { get; set; }

    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public int Total { get; set; }
    public int[]? Pages { get; set; }
    public int PageCount { get; set; }


    public async Task OnGet()
    {
        string cacheKey = $"TagBlogPostBriefList_{Name}_{Current}_{PageSize}";

        if (Name.IsNullOrWhiteSpace())
        {
            async Task<List<TagBrief>?> GetTagFromDb()
            {
                return await _service.GetTagsAsync();
            }

            Tags = await _cacheHelper.GetOrCreateAsync(cacheKey,
                async e => await GetTagFromDb());
        }
        else
        {
            async Task<GetBlogPostBriefListByTagNameResponse?> GetBlogPostsFromDb()
            {
                var factName = Name.IsNullOrWhiteSpace() ? string.Empty : WebUtility.UrlDecode(Name);
                GetBlogPostBriefListByTagNameRequest request = new(factName!, Current, PageSize);
                return await _blogPostService.BlogPostBriefListByTagNameAsync(request);
            }

            var response = await _cacheHelper.GetOrCreateAsync(cacheKey,
                async e => await GetBlogPostsFromDb());

            BlogPosts = response!.Data;
            Total = response.Total;
            PageCount = response.Total.GetPageCount(PageSize);
            Pages = response.Total.GetPages(PageSize, Current, 10);
        }
    }
}