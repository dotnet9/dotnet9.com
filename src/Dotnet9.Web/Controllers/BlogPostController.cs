namespace Dotnet9.Web.Controllers;

[Authorize]
public partial class BlogPostController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICacheService _cacheService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly Dictionary<LoadMoreKind, string> _kindKeys = new()
    {
        { LoadMoreKind.Dotnet, "dotnet" },
        { LoadMoreKind.Front, "Large-front-end" },
        { LoadMoreKind.Database, "database" },
        { LoadMoreKind.MoreLanguage, "more-language" },
        { LoadMoreKind.Course, "course" },
        { LoadMoreKind.Other, "other" }
    };

    private readonly IMapper _mapper;

    public BlogPostController(IBlogPostAppService blogPostAppService,
        IBlogPostRepository blogPostRepository,
        ICategoryRepository categoryRepository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        ICacheService cacheService)
    {
        _blogPostAppService = blogPostAppService;
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    [HttpGet]
    [Route("/{year}/{month}/{slug?}")]
    [AllowAnonymous]
    public async Task<IActionResult> Index(int year, int month, string? slug)
    {
        if (slug.IsNullOrWhiteSpace())
        {
            return NotFound();
        }

        var cacheData = await _cacheService.GetOrCreateAsync(
            $"{nameof(BlogPostController)}-{nameof(Index)}-{year}-{month}-{slug}",
            async () => await _blogPostAppService.FindBySlugAsync(slug!));
        return View(cacheData);
    }

    [HttpGet]
    [Route("/recommend")]
    [AllowAnonymous]
    public async Task<IActionResult> Recommend()
    {
        var cacheData = await _cacheService.GetOrCreateAsync(
            $"{nameof(BlogPostController)}-{nameof(Recommend)}",
            async () => await _blogPostAppService.GetRecommendBlogPostAsync());
        return View(cacheData);
    }

    [HttpGet]
    [Route("/latest")]
    [AllowAnonymous]
    public async Task<IActionResult> LoadLatest(string kind = "", int page = 1)
    {
        var cacheKey = $"{nameof(BlogPostController)}-{nameof(LoadLatest)}-{kind}-{page}";
        var cacheData = await _cacheService.GetAsync<LatestViewModel>(cacheKey);
        if (cacheData != null)
        {
            return PartialView(cacheData);
        }

        var loadKind = LoadMoreKind.Dotnet;
        if (Enum.TryParse(typeof(LoadMoreKind), kind, out var enumKind))
        {
            loadKind = (LoadMoreKind)Enum.Parse(typeof(LoadMoreKind), kind);
        }

        Expression<Func<BlogPost, bool>> whereLambda = x => x.Id > 0;
        if (_kindKeys.ContainsKey(loadKind))
        {
            var categoryKey = _kindKeys[loadKind];
            var dotnetCategoryIds =
                (await _categoryRepository.SelectAsync(x => x.Slug.StartsWith(categoryKey))).Select(x => x.Id);
            whereLambda = x =>
                x.Categories != null && x.Categories.Any(d => dotnetCategoryIds.Contains(d.CategoryId));
        }

        var latest = await _blogPostRepository.SelectBlogPostBriefAsync(8, page, whereLambda, x => x.CreateDate,
            SortDirectionKind.Descending);
        if (!latest.Item1.Any())
        {
            return Json("");
        }

        cacheData = new LatestViewModel
        {
            BlogPosts = _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(latest.Item1)
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return PartialView(cacheData);
    }

    [HttpGet("/q")]
    [AllowAnonymous]
    public async Task<IActionResult> Query(string? keyword, int p = 1)
    {
        var cacheKey = $"{nameof(BlogPostController)}-{nameof(Query)}-{keyword}-{p}";
        var cacheData = await _cacheService.GetAsync<QueryViewModel>(cacheKey);
        if (cacheData != null)
        {
            return PartialView(cacheData);
        }

        Expression<Func<BlogPost, bool>> whereLambda;
        if (keyword.IsNullOrWhiteSpace())
        {
            whereLambda = x => x.Id > 0;
        }
        else
        {
            var queryStr = WebUtility.UrlDecode(keyword);
            whereLambda = x =>
                Regex.IsMatch(x.Title, queryStr!) ||
                Regex.IsMatch(x.Slug, queryStr!) ||
                (x.Original != null && Regex.IsMatch(x.Original, queryStr!)) ||
                Regex.IsMatch(x.BriefDescription, queryStr!) ||
                Regex.IsMatch(x.Content, queryStr!);
        }

        var queryResult = await _blogPostRepository.SelectBlogPostBriefAsync(8, p, whereLambda, x => x.CreateDate,
            SortDirectionKind.Descending);

        cacheData = new QueryViewModel
        {
            Query = keyword,
            PageIndex = p,
            PageCount = (queryResult.Item2 + 8 - 1) / 8,
            Total = queryResult.Item2
        };
        if (queryResult.Item1.Any())
        {
            cacheData.BlogPosts = _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(queryResult.Item1);
        }

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}