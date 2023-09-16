using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9Api.Common;

public class TestController : Controller
{
    [HttpGet("/TestWaterTag")]
    public async Task<IActionResult> TestWaterTag([FromServices] IWebHostEnvironment environment,
        string name = "dotnet9.com")
    {
        string filePath = Path.Combine(environment.WebRootPath, "img", "test.jpg");
        await using FileStream fs = System.IO.File.Open(filePath, FileMode.Open);
        using MemoryStream ms = new MemoryStream();
        await fs.CopyToAsync(ms);
        byte[] img = ImageHelper.WriteWaterTag(ms.ToArray(), name, 80);
        MemoryStream ms2 = new MemoryStream(img);
        return File(ms2, "image/jpg");
    }
}