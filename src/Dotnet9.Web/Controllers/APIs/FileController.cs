using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers.APIs;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<string>> List(string file)
    {
        return await Task.FromResult(new[] { "ddd", "bbb" });
    }
}