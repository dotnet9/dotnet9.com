namespace Dotnet9.RazorPages.Pages.Donations;

public class IndexModel : PageModel
{
    public string? ContentHtml { get; set; }

    public async Task OnGet([FromServices] ICaller caller)
    {
        var dataFromServer = await caller.GetAsync<DonationDto?>($"/api/donations");
        ContentHtml = dataFromServer?.Content.Convert2Html();
    }
}