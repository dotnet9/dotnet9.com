using AutoMapper;
using Dotnet9.AdminAPI.ViewModels.Accounts;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.AdminAPI.Controllers;

[Route("api/account")]
[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserAppService _userAppService;
    private readonly IMapper _mapper;

    public AccountController(IUserAppService userAppService, IMapper mapper)
    {
        _userAppService = userAppService;
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
}