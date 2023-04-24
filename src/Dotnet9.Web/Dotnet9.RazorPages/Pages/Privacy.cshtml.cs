namespace Dotnet9.RazorPages.Pages;

public class PrivacyModel : PageModel
{
    public string? ContentHtml { get; set; }

    public async Task OnGet([FromServices] ICaller caller)
    {
        var dataFromServer = await caller.GetAsync<PrivacyDto?>($"/api/privacies");
        ContentHtml = dataFromServer?.Content.Convert2Html();
    }
}