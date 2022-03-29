using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers
{
    public class AlbumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
