namespace Dotnet9.Web.Pages.BlogPosts;

public class IndexModel : PageModel
{
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly IEventBus _eventBus;
    private readonly IBlogPostService _service;
    private readonly Dotnet9DbContext _dbContext;

    public IndexModel(IBlogPostService service, Dotnet9DbContext dbContext, IEventBus eventBus,
        IDistributedCacheHelper cacheHelper)
    {
        _service = service;
        _dbContext = dbContext;
        _eventBus = eventBus;
        _cacheHelper = cacheHelper;
    }

    [BindProperty(SupportsGet = true)] public string Year { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public string Month { get; set; } = null!;
    [BindProperty(SupportsGet = true)] public string Slug { get; set; } = null!;
    public BlogPostDetails? Current { get; set; }
    public string? ContentHtml { get; set; }

    public async Task OnGet()
    {
        string cacheKey = $"BlogPost_{Slug}";

        async Task<BlogPostDetailsViewModel?> GetBlogPostFromDb()
        {
            var vm = new BlogPostDetailsViewModel();
            vm.Current = await _service.BlogPostDetailsBySlugAsync(Slug);

            return vm;
        }

        var vm = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetBlogPostFromDb());
        Current = vm?.Current;
        ContentHtml = Current?.Content.Convert2Html();
        _eventBus.Publish("Dotnet9.Web.BlogPosts.OnGet", new ReadBlogPostEvent(Slug));
    }
}