using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class ErrorController : Controller
{
    [HttpGet]
    [Route("/error/{0}")]
    public IActionResult Index()
    {
        return Response.StatusCode == StatusCodes.Status404NotFound ? View("/Views/Error/Index.cshtml") : View();
    }
}