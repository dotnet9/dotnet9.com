namespace Dotnet9.Web.Controllers.APIs;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class FileController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<string>> List(string file)
    {
        return await Task.FromResult(new[] { "ddd", "bbb" });
    }
}