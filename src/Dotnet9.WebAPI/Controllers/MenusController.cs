namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenusController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ResponseResult<List<MenuItemDto>?>> ListAsync()
    {
        var testMenusStr =
            await System.IO.File.ReadAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "menu.json"));
        return ResponseResult<List<MenuItemDto>?>.GetSuccess(testMenusStr?.ParseJson<List<MenuItemDto>>(true));
    }
}