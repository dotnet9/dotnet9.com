using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.About;

public class MenuVertical : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}