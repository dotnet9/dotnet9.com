namespace Dotnet9.Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBlogPostService _blogPostService;
    private readonly IMemoryCacheHelper _cacheHelper;

    public IndexModel(ILogger<IndexModel> logger, IBlogPostService blogPostService, IMemoryCacheHelper cacheHelper)
    {
        _logger = logger;
        _blogPostService = blogPostService;
        _cacheHelper = cacheHelper;
    }

    public string? CategoryName { get; set; }

    public List<BlogPostBrief>? BlogPosts { get; set; }

    [BindProperty(SupportsGet = true)] public string Slug { get; set; } = null!;

    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public int Total { get; set; }
    public int[]? Pages { get; set; }
    public int PageCount { get; set; }


    public async Task OnGet()
    {
        string cacheKey = $"CategoryBlogPostBriefList_{Slug}_{Current}_{PageSize}";

        async Task<GetBlogPostBriefListByCategorySlugResponse?> GetBlogPostsFromDb()
        {
            var request = new GetBlogPostBriefListByCategorySlugRequest(Slug, Current, PageSize);
            return await _blogPostService.GetBlogPostBriefListByCategorySlugAsync(request);
        }

        var response = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetBlogPostsFromDb());
        
        CategoryName = response!.CategoryName;
        BlogPosts = response.Data;
        Total = response.Total;
        PageCount = response.Total.GetPageCount(PageSize);
        Pages = response.Total.GetPages(PageSize, Current, 10);
    }
}