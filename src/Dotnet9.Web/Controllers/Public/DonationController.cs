using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Application.Contracts.Donations;
using Dotnet9.Web.ViewModels.Donations;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
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
        const string cacheKey = $"{nameof(DonationController)}-{nameof(Index)}";
        var cacheData = await _cacheService.GetAsync<DonationViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new DonationViewModel
        {
            Donation = await _donationAppService.GetAsync()
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}