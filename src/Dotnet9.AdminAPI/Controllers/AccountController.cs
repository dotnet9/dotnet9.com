using Dotnet9.AdminAPI.ViewModels.Accounts;
using Dotnet9.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.AdminAPI.Controllers;

[Route("api/account")]
[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpGet("checklogin")]
    public async Task<LoginStatusViewModel> CheckLogin()
    {
        var isLogin = HttpContext.User.Identity is { IsAuthenticated: true };
        return new LoginStatusViewModel
        {
            IsLogin = isLogin,
            IsInit = await _userRepository.GetAsync(x => x.Id > 0) != null
        };
    }
}