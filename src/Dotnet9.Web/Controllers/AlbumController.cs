using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
using Dotnet9.Web.Caches;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class AlbumController : Controller
{
    private readonly IAlbumAppService _albumAppService;
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly ICacheService _cacheService;

    public AlbumController(IAlbumAppService albumAppService, IBlogPostAppService blogPostAppService,
        ICacheService cacheService)
    {
        _albumAppService = albumAppService;
        _blogPostAppService = blogPostAppService;
        _cacheService = cacheService;
    }

    [HttpGet]
    [Route("album/{slug?}")]
    public async Task<IActionResult> Index(string? slug)
    {
        if (slug.IsNullOrWhiteSpace()) return NotFound();

        var cacheKey = $"{nameof(AlbumController)}-{nameof(Index)}-{slug}";
        var cacheData = await _cacheService.GetAsync<AlbumViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = await _albumAppService.GetAlbumAsync(slug!);
        if (cacheData == null) return NotFound();

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}