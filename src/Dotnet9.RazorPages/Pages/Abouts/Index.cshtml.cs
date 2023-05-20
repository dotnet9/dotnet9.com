namespace Dotnet9.RazorPages.Pages.Abouts;

public class IndexModel : PageModel
{
    public string? AboutContentHtml { get; set; }
    public SiteInfoDto? SiteInfo { get; set; }

    public async Task OnGet([FromServices] ISystemClientService systemClientService,
        [FromServices] AboutService aboutService)
    {
        SiteInfo = await systemClientService.GetSiteInfoAsync();
        var about = await aboutService.GetAsync();
        AboutContentHtml = about?.Content.Convert2Html();
    }
}