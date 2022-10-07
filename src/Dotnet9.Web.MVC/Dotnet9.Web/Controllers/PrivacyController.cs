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
        var cacheData = await _cacheService.GetOrCreateAsync(
            $"{nameof(PrivacyController)}-{nameof(Index)}",
            async () => new PrivacyViewModel
            {
                Privacy = await _privacyAppService.GetAsync()
            });
        return View(cacheData);
    }
}