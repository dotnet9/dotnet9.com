using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.Abouts;

public class SidebarRight : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}