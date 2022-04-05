using Dotnet9.Application.Contracts.Donations;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ViewModels.Donations;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<IActionResult> Index()
    {
        var cacheKey = $"{nameof(DonationController)}-{nameof(Index)}";
        var cacheData = await _cacheService.GetAsync<DonationViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new DonationViewModel
        {
            Donation = await _donationAppService.GetAsync()
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData);

        return View(cacheData);
    }
}