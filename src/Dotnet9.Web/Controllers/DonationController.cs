namespace Dotnet9.Web.Controllers;

public class DonationController : Controller
{
    private readonly ICacheService _cacheService;
    private readonly IDonationAppService _donationAppService;

    public DonationController(IDonationAppService donationAppService, ICacheService cacheService)
    {
        _donationAppService = donationAppService;
        _cacheService = cacheService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cacheData = await _cacheService.GetOrCreateAsync(
            $"{nameof(DonationController)}-{nameof(Index)}",
            async () => new DonationViewModel
            {
                Donation = await _donationAppService.GetAsync()
            });
        return View(cacheData);
    }
}