namespace Dotnet9.Web.Controllers;

public class TagController : Controller
{
    private readonly ICacheService _cacheService;
    private readonly ITagAppService _tagAppService;

    public TagController(ITagAppService tagAppService, ICacheService cacheService)
    {
        _tagAppService = tagAppService;
        _cacheService = cacheService;
    }

    [HttpGet]
    [Route("tag/{name?}")]
    public async Task<IActionResult> Index(string? name)
    {
        var cacheData = await _cacheService.GetOrCreateAsync(
            $"{nameof(TagController)}-{nameof(Index)}-{name}",
            async () => await _tagAppService.GetTagAsync(name));
        return View(cacheData);
    }
}