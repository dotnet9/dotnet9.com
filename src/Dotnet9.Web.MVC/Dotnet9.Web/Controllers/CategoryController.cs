namespace Dotnet9.Web.Controllers;

[Authorize]
public partial class CategoryController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly ICacheService _cacheService;
    private readonly ICategoryAppService _categoryAppService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryAppService categoryAppService, ICategoryRepository categoryRepository,
        IBlogPostAppService blogPostAppService,
        ICacheService cacheService, IMapper mapper)
    {
        _categoryAppService = categoryAppService;
        _categoryRepository = categoryRepository;
        _blogPostAppService = blogPostAppService;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("cat/{slug?}")]
    [AllowAnonymous]
    public async Task<IActionResult> Index(string? slug)
    {
        var cacheData = await _cacheService.GetOrCreateAsync(
            $"{nameof(CategoryController)}-{nameof(Index)}-{slug}",
            async () => await _categoryAppService.GetCategoryAsync(slug!));
        return View(cacheData);
    }
}