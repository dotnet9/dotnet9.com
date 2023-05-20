namespace Dotnet9.RazorPages.Pages.Albums;

public class BlogModel : PageModel
{
    [BindProperty(SupportsGet = true)] public string Slug { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;
    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

    public GetBlogListByAlbumSlugResponse? RequestAlbumBlogResponse { get; set; }
    public int[]? Pages { get; set; }

    public List<BlogBrief>? Blogs { get; set; }
    
    public async Task OnGet([FromServices] AlbumService albumService)
    {
        RequestAlbumBlogResponse =
            await albumService.GetBlogBriefListByAlbumSlugAsync(Slug, PageSize, Current);
        Blogs = RequestAlbumBlogResponse?.Records;
        Pages = Enumerable.Range(1, RequestAlbumBlogResponse!.TotalPage).ToArray();
    }
}