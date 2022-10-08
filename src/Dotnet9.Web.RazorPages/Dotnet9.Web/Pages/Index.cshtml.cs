namespace Dotnet9.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBlogPostService _blogPostService;

    public IndexModel(ILogger<IndexModel> logger, IBlogPostService blogPostService)
    {
        _logger = logger;
        _blogPostService = blogPostService;
    }

    public List<BlogPostBrief>? BlogPosts { get; set; }

    [BindProperty(SupportsGet = true)] public string? Keywords { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public int Total { get; set; }
    public int[]? Pages { get; set; }


    public async Task OnGet()
    {
        var request = new GetBlogPostBriefListRequest(Keywords, Current, PageSize);
        var response = await _blogPostService.GetBlogPostBriefListAsync(request);
        BlogPosts = response.Data;
        Total = response.Total;
        Pages = response.Total.GetPages(PageSize, Current, 5);
    }
}