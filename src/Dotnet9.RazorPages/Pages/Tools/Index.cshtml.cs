

namespace Dotnet9.RazorPages.Pages.Tools;

public class IndexModel : PageModel
{
    public List<ToolItem>? Items;

    public async Task OnGet([FromServices] ISystemClientService systemClientService)
    {
        var siteInfo  = await systemClientService.GetSiteInfoAsync();
        Items = new List<ToolItem>()
        {
            new("时间戳", $"{siteInfo?.AssetsRemotePath}/tools/timestamp.png", "时间戳转换，页面使用Blazor组件，暂时无法交互", "/tools/timestamp", 0)
        };
    }
}