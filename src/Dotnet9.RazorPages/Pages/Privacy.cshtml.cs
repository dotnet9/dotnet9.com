namespace Dotnet9.RazorPages.Pages;

public class PrivacyModel : PageModel
{
    public string? ContentHtml { get; set; }
    public SiteInfoDto? SiteInfo { get; set; }

    public async Task OnGet([FromServices] ISystemClientService systemClientService,
        [FromServices] PrivacyService privacyService)
    {
        SiteInfo = await systemClientService.GetSiteInfoAsync();
        var dataFromServer = await privacyService.GetAsync();
        ContentHtml = dataFromServer?.Content.Convert2Html();
    }
}