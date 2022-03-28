using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}