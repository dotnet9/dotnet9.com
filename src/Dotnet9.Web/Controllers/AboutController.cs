using Dotnet9.Application.Contracts.Abouts;
using Dotnet9.Web.ViewModels.Abouts;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class AboutController : Controller
{
    private readonly IAboutAppService _aboutAppService;

    public AboutController(IAboutAppService aboutAppService)
    {
        _aboutAppService = aboutAppService;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new AboutViewModel
        {
            About = await _aboutAppService.GetAsync()
        };
        return View(vm);
    }
}