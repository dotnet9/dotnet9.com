using Dotnet9.AdminAPI.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.AdminAPI.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<LoginStatusViewModel> CheckLogin()
    {
        return await Task.FromResult(LoginStatusViewModel.Success());
    }
}