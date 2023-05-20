namespace Dotnet9.RazorPages.Pages.Tags;

public class BlogModel : PageModel
{
    [BindProperty(SupportsGet = true)] public string Name { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public GetBlogListByTagNameResponse? RequestResponse { get; set; }
    public int[]? Pages { get; set; }

    public List<BlogBrief>? Blogs { get; set; }


    public async Task OnGet([FromServices] TagService tagService)
    {
        RequestResponse = await tagService.GetBlogBriefListByTagNameAsync(Name, PageSize, Current);

        Blogs = RequestResponse?.Records;
        Pages = Enumerable.Range(1, RequestResponse!.TotalPage).ToArray();
    }
}