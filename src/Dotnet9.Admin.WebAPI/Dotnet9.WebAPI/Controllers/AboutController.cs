namespace Dotnet9.WebAPI.Controllers;

[Route("api/abouts")]
[ApiController]
public class AboutController : ControllerBase
{
    private const string GetAboutCacheKey = "AboutController.GetAbout";
    private readonly IMemoryCacheHelper _cacheHelper;
    private readonly Dotnet9DbContext _dbContext;
    private readonly Dotnet9DbContext _dotnet9DbContext;
    private readonly AboutManager _manager;
    private readonly IAboutRepository _repository;
    private readonly IOptionsSnapshot<SiteOptions> _siteOptions;

    public AboutController(Dotnet9DbContext dbContext, IAboutRepository repository, AboutManager manager,
        IMemoryCacheHelper cacheHelper, Dotnet9DbContext dotnet9DbContext, IOptionsSnapshot<SiteOptions> siteOptions)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
        _cacheHelper = cacheHelper;
        _dotnet9DbContext = dotnet9DbContext;
        _siteOptions = siteOptions;
    }

    [HttpPost("/api/report")]
    public async Task<string?> Report()
    {
        return null;
    }

    [HttpGet]
    [Route("/api")]
    public async Task<AboutSiteViewModel> Details()
    {
        int blogPostCount = await _dotnet9DbContext.BlogPosts!.CountAsync();
        int commentCount = 0; // TODO
        int albumCount = await _dotnet9DbContext.Albums!.CountAsync();
        int categoryCount = await _dotnet9DbContext.Categories!.CountAsync();
        int tagCount = await _dotnet9DbContext.Tags!.CountAsync();
        int viewCount = 0;
        return new AboutSiteViewModel(blogPostCount, commentCount, albumCount, categoryCount, tagCount, viewCount,
            _siteOptions.Value);
    }

    [HttpGet]
    [NoWrapper]
    public async Task<ActionResult<AboutDto>> Get()
    {
        async Task<AboutDto?> GetAboutFromDb()
        {
            About? aboutFromDb = await _repository.GetAsync();
            return aboutFromDb == null ? null : new AboutDto(aboutFromDb.Content!);
        }

        AboutDto? about = await _cacheHelper.GetOrCreateAsync(GetAboutCacheKey,
            async e => await GetAboutFromDb());
        if (about == null)
        {
            return NotFound();
        }

        return about;
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ActionResult<ResponseResult<bool>>> AddOrUpdate(AddOrUpdateAboutRequest request)
    {
        About? about = await _repository.GetAsync();
        if (about == null)
        {
            about = _manager.Create(request.Content);
            await _dbContext.AddAsync(about);
        }
        else
        {
            about.ChangeContent(request.Content);
        }

        await _dbContext.SaveChangesAsync();
        _cacheHelper.Remove(GetAboutCacheKey);
        return ResponseResult<bool>.GetSuccess(true);
    }
}