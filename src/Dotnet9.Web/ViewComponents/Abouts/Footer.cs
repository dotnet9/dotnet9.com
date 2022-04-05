using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ViewModels.Homes;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.Abouts;

public class Footer : ViewComponent
{
    private readonly ICacheService _cacheService;
    private readonly IUrlLinkAppService _urlLinkAppService;

    public Footer(IUrlLinkAppService urlLinkAppService, ICacheService cacheService)
    {
        _urlLinkAppService = urlLinkAppService;
        _cacheService = cacheService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cacheKey = $"{nameof(Footer)}";
        var cacheData = await _cacheService.GetAsync<FooterViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new FooterViewModel
        {
            FriendLinks = await _urlLinkAppService.ListAllAsync()
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData);

        return View(cacheData);
    }
}