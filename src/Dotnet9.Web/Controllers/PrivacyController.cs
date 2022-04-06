using Dotnet9.Application.Contracts.Privacies;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ViewModels.Privacies;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class PrivacyController : Controller
{
    private readonly ICacheService _cacheService;
    private readonly IPrivacyAppService _privacyAppService;

    public PrivacyController(IPrivacyAppService privacyAppService, ICacheService cacheService)
    {
        _privacyAppService = privacyAppService;
        _cacheService = cacheService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cacheKey = $"{nameof(PrivacyController)}-{nameof(Index)}";
        var cacheData = await _cacheService.GetAsync<PrivacyViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new PrivacyViewModel
        {
            Privacy = await _privacyAppService.GetAsync()
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}