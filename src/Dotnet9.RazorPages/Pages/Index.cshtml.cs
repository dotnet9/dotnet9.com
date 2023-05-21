namespace Dotnet9.RazorPages.Pages;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)] public string? Keywords { get; set; }
    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public GetBlogListByKeywordsResponse? RequestResponse { get; set; }
    public int[]? Pages { get; set; }

    public SiteInfoDto? SiteInfo { get; set; }
    public string UrlSuffix { get; set; } = null!;
    public List<BlogBrief>? Blogs { get; private set; }

    public async Task OnGet([FromServices] ISystemClientService systemClientService,
        [FromServices] BlogService blogService)
    {
        SiteInfo = await systemClientService.GetSiteInfoAsync();
        UrlSuffix = "";
        if ("请输入关键字词" == Keywords)
        {
            Keywords = string.Empty;
        }
        if (!Keywords.IsNullOrWhiteSpace())
        {
            UrlSuffix += $"?keywords={WebUtility.UrlEncode(Keywords)}";
        }

        RequestResponse = await blogService.GetBlogBriefListByKeywordsAsync(Keywords, PageSize, Current);

        Blogs = RequestResponse?.Records;
        Pages = Enumerable.Range(1, RequestResponse!.TotalPage).ToArray();
    }
}