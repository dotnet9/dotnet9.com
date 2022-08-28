namespace Dotnet9.Web.ViewComponents.Blogs;

public class RandomBlogPosts : ViewComponent
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public RandomBlogPosts(IBlogPostRepository blogPostRepository, ICacheService cacheService, IMapper mapper)
    {
        _blogPostRepository = blogPostRepository;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        const string cacheKey = $"{nameof(LatestBlogPosts)}-{nameof(InvokeAsync)}";
        var cacheData = await _cacheService.GetAsync<LatestViewModel>(cacheKey);
        if (cacheData != null)
        {
            return View(cacheData);
        }

        var latest = await _blogPostRepository.SelectBlogPostBriefAsync(8, 1, x => x.Id > 0, x => x.CreateDate,
            SortDirectionKind.Descending);
        cacheData = new LatestViewModel
        {
            BlogPosts = _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(latest.Item1)
        };
        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}