using System.Net;

namespace Dotnet9.RazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty(SupportsGet = true)] public string? Keywords { get; set; }
    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public GetBlogListByKeywordsResponse? RequestResponse { get; set; }
    public int[]? Pages { get; set; }

    public SiteInfo? SiteInfo { get; private set; }
    public string UrlSuffix { get; set; }
    public List<BlogBrief>? Blogs { get; private set; }


    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet([FromServices] ICaller caller)
    {
        UrlSuffix = $"";
        if (!Keywords.IsNullOrWhiteSpace())
        {
            UrlSuffix += $"&keywords={WebUtility.UrlEncode(Keywords)}";
        }

        SiteInfo = await caller.GetAsync<SiteInfo>("/api/systems");
        var url = $"/api/blogs?";
        if (!Keywords.IsNullOrWhiteSpace())
        {
            url += $"keywords={WebUtility.UrlEncode(Keywords)}";
            url += "&";
        }

        url += $"page={Current}&pageSize={PageSize}";

        RequestResponse = await caller.GetAsync<GetBlogListByKeywordsResponse>(url);

        Blogs = RequestResponse?.Records;
        Pages = Enumerable.Range(1, RequestResponse!.TotalPage).ToArray();
    }
}