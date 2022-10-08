namespace Dotnet9.Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBlogPostService _blogPostService;

    public IndexModel(ILogger<IndexModel> logger, IBlogPostService blogPostService)
    {
        _logger = logger;
        _blogPostService = blogPostService;
    }

    public string? CategoryName { get; set; }

    public List<BlogPostBrief>? BlogPosts { get; set; }

    [BindProperty(SupportsGet = true)] public string Slug { get; set; } = null!;

    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public int Total { get; set; }
    public int[]? Pages { get; set; }


    public async Task OnGet()
    {
        var request = new GetBlogPostBriefListByCategorySlugRequest(Slug, Current, PageSize);
        var response = await _blogPostService.GetBlogPostBriefListByCategorySlugAsync(request);
        CategoryName = response.CategoryName;
        BlogPosts = response.Data;
        Total = response.Total;
        Pages = response.Total.GetPages(PageSize, Current, 5);
    }
}