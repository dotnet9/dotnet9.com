using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Web.ViewModels.Homes;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.Abouts;

public class Footer : ViewComponent
{
    private readonly IUrlLinkAppService _urlLinkAppService;

    public Footer(IUrlLinkAppService urlLinkAppService)
    {
        _urlLinkAppService = urlLinkAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new FooterViewModel();
        vm.FriendLinks = await _urlLinkAppService.ListAllAsync();
        return await Task.FromResult(View(vm));
    }
}