namespace Dotnet9.RazorPages.Pages.Blogs;

public class ArchivesModel : PageModel
{
    public List<BlogArchive>? BlogArchives { get; set; }
    

    public async Task OnGet([FromServices] BlogService blogService)
    {
        BlogArchives =
            await blogService.GetArchivesAsync();
    }
}