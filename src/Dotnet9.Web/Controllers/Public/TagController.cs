using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Application.Contracts.Tags;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
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
        var cacheKey = $"{nameof(TagController)}-{nameof(Index)}-{name}";
        var cacheData = await _cacheService.GetAsync<TagViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = await _tagAppService.GetTagAsync(name);
        if (cacheData == null) return NotFound();

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}