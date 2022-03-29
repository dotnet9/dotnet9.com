using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class ErrorController : Controller
{
    [Route("/error/{0}")]
    public IActionResult Index()
    {
        if (Response.StatusCode == StatusCodes.Status404NotFound)
        {
            return View("/Views/Error/Index.cshtml");
        }
        return View();
    }
}