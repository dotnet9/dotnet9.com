using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9Tools.Controllers;

[Route("/admin/[controller]/[action]")]
[ApiController]
[Authorize]
public class BaseAdminController : ControllerBase
{
}