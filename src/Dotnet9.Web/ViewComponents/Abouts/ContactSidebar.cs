namespace Dotnet9.Web.ViewComponents.Abouts;

public class ContactSidebar : ViewComponent
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICacheService _cacheService;

    public ContactSidebar(IBlogPostRepository blogPostRepository, ICacheService cacheService)
    {
        _blogPostRepository = blogPostRepository;
        _cacheService = cacheService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        const string cacheKey = $"{nameof(ContactSidebar)}-{nameof(InvokeAsync)}";
        var cacheData = await _cacheService.GetAsync<ContactSidebarViewModel>(cacheKey);
        if (cacheData != null)
        {
            return View(cacheData);
        }

        var maxBlogPostId = await _blogPostRepository.GetMaxIdAsync();
        cacheData = new ContactSidebarViewModel
        {
            TotalPostCount = maxBlogPostId,
            LatestPostTime = (await _blogPostRepository.GetBlogPostBriefAsync(x => x.Id == maxBlogPostId,
                x => x.CreateDate, SortDirectionKind.Descending))?.CreateDate
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}