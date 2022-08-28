namespace Dotnet9.Web.ViewComponents.Abouts;

public class MenuVertical : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}