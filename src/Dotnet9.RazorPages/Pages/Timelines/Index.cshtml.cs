namespace Dotnet9.RazorPages.Pages.Timelines;

public class IndexModel : PageModel
{
    public List<TimelineDto>? Timelines { get; set; }


    public async Task OnGet([FromServices] TimelineService timelineService)
    {
        Timelines = await timelineService.GetAllAsync();
    }
}