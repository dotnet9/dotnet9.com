namespace Dotnet9.RazorPages.Pages.Blogs;

public class DetailsModel : PageModel
{
    [BindProperty(SupportsGet = true)] public string Slug { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public int Year { get; set; }
    [BindProperty(SupportsGet = true)] public int Month { get; set; }

    public SiteInfoDto? SiteInfo { get; set; }
    public BlogDetails? Blog { get; set; }
    public string? BlogContentHtml { get; set; }


    public async Task OnGet([FromServices] ISystemClientService systemClientService,
        [FromServices] BlogService blogService)
    {
        SiteInfo = await systemClientService.GetSiteInfoAsync();
        Blog = await blogService.GetBlogDetailsBySlugAsync(Slug);
        BlogContentHtml = Blog?.Content.Convert2Html();
    }
}