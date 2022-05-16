using AutoMapper;
using Dotnet9.Application.Contracts.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dotnet9.Web.ViewModels.Accounts;

// ReSharper disable once CheckNamespace
namespace Dotnet9.Web.Controllers;

[Route("api/account")]
[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IUserAppService _userAppService;

    public AccountController(IUserAppService userAppService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _userAppService = userAppService;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("checkLogin")]
    public async Task<LoginStatusViewModel> CheckLogin()
    {
        var isLogin = HttpContext.User.Identity is {IsAuthenticated: true};
        return new LoginStatusViewModel
        {
            IsLogin = isLogin,
            IsInit = await _userAppService.ExistAdminAccountAsync()
        };
    }

    [AllowAnonymous]
    [HttpPost("createAdminAccount")]
    public async Task<IActionResult> CreateAdminAccount(AdminAccountForCreationViewModel request)
    {
        var adminAccountForDb = _mapper.Map<AdminAccountForCreationViewModel, UserForCreationDto>(request);
        await _userAppService.CreateAdminAccountAsync(adminAccountForDb);
        return Ok("初始化账号成功");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task Login([FromBody] AccountLoginViewModel request)
    {
        var accountLoginForDb = _mapper.Map<AccountLoginViewModel, UserForLoginDto>(request);
        var res = await _userAppService.LoginAsync(accountLoginForDb);
        if (!res.IsSuccess) throw new UserException(res.Message);
        var identity = new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Name, request.Account)
        }, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        if (_httpContextAccessor.HttpContext != null)
            await _httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);
    }

    [HttpGet("logout")]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync();
    }
}