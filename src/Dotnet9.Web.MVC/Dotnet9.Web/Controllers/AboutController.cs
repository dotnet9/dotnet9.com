namespace Dotnet9.Web.Controllers;

public class AboutController : Controller
{
    private readonly IAboutAppService _aboutAppService;
    private readonly ICacheService _cacheService;

    public AboutController(IAboutAppService aboutAppService, ICacheService cacheService)
    {
        _aboutAppService = aboutAppService;
        _cacheService = cacheService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cacheData = await _cacheService.GetOrCreateAsync($"{nameof(AboutController)}-{nameof(Index)}", async () =>
            new AboutViewModel
            {
                About = await _aboutAppService.GetAsync()
            });
        return View(cacheData);
    }
}