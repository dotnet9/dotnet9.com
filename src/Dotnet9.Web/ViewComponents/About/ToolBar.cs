using Dotnet9.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.About;

public class ToolBar : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(bool showMenu)
    {
        var vm = new ToolBarViewModel {ShowMenu = showMenu};
        return await Task.FromResult(View(vm));
    }
}