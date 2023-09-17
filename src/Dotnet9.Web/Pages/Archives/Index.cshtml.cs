namespace Dotnet9.Web.Pages.Archives;

public class IndexModel : PageModel
{
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly IBlogPostService _service;

    public IndexModel(IBlogPostService service,
        IDistributedCacheHelper cacheHelper)
    {
        _service = service;
        _cacheHelper = cacheHelper;
    }

    public List<BlogPostArchiveItem>? BlogPosts { get; set; }

    public async Task OnGet()
    {
        string cacheKey = "BlogPostArchive";

        async Task<List<BlogPostArchiveItem>?> GetBlogPostsFromDb()
        {
            return await _service.ArchivesAsync();
        }

        BlogPosts = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetBlogPostsFromDb());
    }
}