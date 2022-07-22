namespace Dotnet9.Web.Controllers.APIs;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    public ActionResult<LoginResult> Login(LoginRequest loginRequest)
    {
        if ("dotnet9" == loginRequest.UserName && "dotnet9" == loginRequest.Password)
        {
            var processes = Process.GetProcesses().Select(p => new ProcessInfo(p.Id, p.ProcessName, p.WorkingSet64))
                .ToArray();
            return new LoginResult(true, processes);
        }

        return new LoginResult(false);
    }
}