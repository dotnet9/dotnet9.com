namespace Dotnet9.RazorPages.Pages.Tags;

public class IndexModel : PageModel
{
    public List<TagBrief>? Tags { get; set; }
    
    public async Task OnGet([FromServices] TagService tagService)
    {
        Tags = await tagService.GetAllAsync();
    }
}