using Dotnet9.AdminAPI.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.AdminAPI.Controllers;

[Route("api/account")]
[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("checklogin")]
    public async Task<LoginStatusViewModel> CheckLogin()
    {
        return await Task.FromResult(LoginStatusViewModel.Success());
    }
}