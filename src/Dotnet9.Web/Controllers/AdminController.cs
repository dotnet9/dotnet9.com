using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class AdminController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}