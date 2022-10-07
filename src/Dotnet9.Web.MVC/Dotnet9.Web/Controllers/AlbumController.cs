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
        var cacheData = await _cacheService.GetOrCreateAsync($"{nameof(AlbumController)}-{nameof(Index)}-{slug}",
            async () => await _albumAppService.GetAlbumAsync(slug!));
        return View(cacheData);
    }
}