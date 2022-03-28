using Dotnet9.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.About;

public class Footer : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new FooterViewModel();
        return await Task.FromResult(View(vm));
    }
}