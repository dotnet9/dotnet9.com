namespace Dotnet9.Web.Pages.BlogPosts;

public class IndexModel : PageModel
{
    private readonly IBlogPostService _service;
    private readonly IMemoryCacheHelper _cacheHelper;

    public IndexModel(IBlogPostService service, IMemoryCacheHelper cacheHelper)
    {
        _service = service;
        _cacheHelper = cacheHelper;
    }

    [BindProperty(SupportsGet = true)] public string Year { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public string Month { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public string Slug { get; set; } = null!;
    public BlogPostDetails? BlogPost { get; set; }
    public string? ContentHtml { get; set; }

    public async Task OnGet()
    {
        string cacheKey = $"BlogPost_{Slug}";

        async Task<BlogPostDetails?> GetBlogPostFromDb()
        {
            return await _service.GetBlogPostDetailsBySlugAsync(Slug);
        }

        BlogPost = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetBlogPostFromDb());
        ContentHtml = BlogPost?.Content.Convert2Html();
    }
}