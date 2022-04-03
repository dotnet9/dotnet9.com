using Dotnet9.Application.Contracts.Donations;
using Dotnet9.Web.ViewModels.Donations;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class DonationController : Controller
{
    private readonly IDonationAppService _donationAppService;

    public DonationController(IDonationAppService donationAppService)
    {
        _donationAppService = donationAppService;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new DonationViewModel
        {
            Donation = await _donationAppService.GetAsync()
        };
        return View(vm);
    }
}