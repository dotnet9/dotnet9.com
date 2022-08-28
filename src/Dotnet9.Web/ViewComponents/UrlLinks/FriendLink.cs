using Dotnet9.Web.ViewModels.UrlLinks;

namespace Dotnet9.Web.ViewComponents.UrlLinks;

public class FriendLink : ViewComponent
{
    private readonly ICacheService _cacheService;
    private readonly IUrlLinkAppService _urlLinkAppService;

    public FriendLink(IUrlLinkAppService urlLinkAppService, ICacheService cacheService)
    {
        _urlLinkAppService = urlLinkAppService;
        _cacheService = cacheService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        const string cacheKey = $"{nameof(FriendLink)}";
        var cacheData = await _cacheService.GetAsync<FriendLinkViewModel>(cacheKey);
        if (cacheData != null)
        {
            return View(cacheData);
        }

        cacheData = new FriendLinkViewModel
        {
            FriendLinks = await _urlLinkAppService.ListAllAsync()
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}