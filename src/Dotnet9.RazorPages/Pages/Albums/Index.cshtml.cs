namespace Dotnet9.RazorPages.Pages.Albums;

public class IndexModel : PageModel
{
    public List<AlbumBrief>? AlbumList { get; set; }

    public async Task OnGet([FromServices] AlbumService albumService)
    {
        AlbumList =
            await albumService.GetBriefAsync();
    }
}