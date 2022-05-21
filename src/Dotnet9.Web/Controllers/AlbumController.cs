using AutoMapper;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Domain.Albums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

[Authorize]
public partial class AlbumController : Controller
{
    private readonly IAlbumAppService _albumAppService;
    private readonly IAlbumRepository _albumRepository;
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public AlbumController(IAlbumAppService albumAppService, IAlbumRepository albumRepository,
        IBlogPostAppService blogPostAppService,
        ICacheService cacheService, IMapper mapper)
    {
        _albumAppService = albumAppService;
        _albumRepository = albumRepository;
        _blogPostAppService = blogPostAppService;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("album/{slug?}")]
    [AllowAnonymous]
    public async Task<IActionResult> Index(string? slug)
    {
        var cacheKey = $"{nameof(AlbumController)}-{nameof(Index)}-{slug}";
        var cacheData = await _cacheService.GetAsync<AlbumViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = await _albumAppService.GetAlbumAsync(slug!);
        if (cacheData == null) return NotFound();

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}