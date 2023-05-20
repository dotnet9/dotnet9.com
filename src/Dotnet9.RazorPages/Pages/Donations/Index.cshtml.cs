namespace Dotnet9.RazorPages.Pages.Donations;

public class IndexModel : PageModel
{
    public string? ContentHtml { get; set; }
    public SiteInfoDto? SiteInfo { get; set; }

    public async Task OnGet([FromServices] ISystemClientService systemClientService,
        [FromServices] DonationService donationService)
    {
        SiteInfo = await systemClientService.GetSiteInfoAsync();
        var dataFromServer = await donationService.GetAllAsync();
        ContentHtml = dataFromServer?.Content.Convert2Html();
    }
}