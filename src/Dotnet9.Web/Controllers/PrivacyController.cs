using Dotnet9.Application.Contracts.Privacies;
using Dotnet9.Web.ViewModels.Privacies;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class PrivacyController : Controller
{
    private readonly IPrivacyAppService _privacyAppService;

    public PrivacyController(IPrivacyAppService privacyAppService)
    {
        _privacyAppService = privacyAppService;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new PrivacyViewModel
        {
            Privacy = await _privacyAppService.GetAsync()
        };
        return View(vm);
    }
}