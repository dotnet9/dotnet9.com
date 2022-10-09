namespace Dotnet9.Web.Pages.Abouts;

public class IndexModel : PageModel
{
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly IAboutRepository _repository;

    public IndexModel(IAboutRepository repository,
        IDistributedCacheHelper cacheHelper)
    {
        _repository = repository;
        _cacheHelper = cacheHelper;
    }

    public string? ContentHtml { get; set; }

    public async Task OnGet()
    {
        string cacheKey = "About";

        async Task<About?> GetAboutFromDb()
        {
            return await _repository.GetAsync();
        }

        About? about = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetAboutFromDb());
        ContentHtml = about?.Content.Convert2Html();
    }
}