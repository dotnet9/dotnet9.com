using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IdManager _manager;
    private readonly IIdRepository _repository;

    public LoginController(IdManager manager, IIdRepository repository)
    {
        _manager = manager;
        _repository = repository;
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserResponse>> CurrentUser()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        User? user = await _repository.FindByIdAsync(Guid.Parse(userId!));
        if (user == null)
        {
            return NotFound();
        }

        return new UserResponse
        {
            UserId = user.Id, Name = user.UserName, Phone = user.PhoneNumber, Avatar =
                "https://img1.dotnet9.com/site/logo.png"
        };
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseResult<bool>> ChangePassword(ChangeMyPasswordRequest req)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        User? user = await _repository.FindByIdAsync(Guid.Parse(userId!));
        if (user == null)
        {
            return ResponseResult<bool>.GetError(HttpStatusCode.BadRequest, "不存在的用户");
        }

        SignInResult checkResult = await _repository.CheckForSignInAsync(user, req.OldPassword, false);
        if (!checkResult.Succeeded)
        {
            return checkResult.IsLockedOut
                ? ResponseResult<bool>.GetError(HttpStatusCode.Locked, "用户已经被锁定")
                : ResponseResult<bool>.GetError(HttpStatusCode.BadRequest, $"修改失败：{checkResult}");
        }

        IdentityResult changeResult = await _repository.ChangePasswordAsync(user.Id, req.NewPassword);
        return changeResult.Succeeded
            ? true
            : ResponseResult<bool>.GetError(HttpStatusCode.BadRequest, "修改失败，请检查原密码是否输入正确");
    }

    [AllowAnonymous]
    [HttpPost]
    [NoWrapper]
    public async Task<LoginResponse> Account(LoginRequest req)
    {
        (SignInResult Result, string? Token) loginResult;
        if (LoginRequestType.Account == req.Type)
        {
            loginResult = await _manager.LoginByUserNameAndPwdAsync(req.UserName, req.Password);
        }
        else
        {
            loginResult = await _manager.LoginByPhoneAndPwdAsync(req.UserName, req.Password);
        }

        string status = loginResult.Result.Succeeded ? "ok" : "error";
        string currentAuthority = "guest";
        string? token = loginResult.Token;
        if (loginResult.Result.Succeeded)
        {
            if (LoginRequestType.Account != req.Type)
            {
                return new LoginResponse(true, "ok", req.Type!, currentAuthority, token);
            }

            User? user = await _repository.FindByNameAsync(req.UserName);
            IList<string> roles = await _repository.GetRolesAsync(user!);
            currentAuthority = roles.Contains(UserRoleConst.Admin) ? "admin" : "user";

            return new LoginResponse(true, "ok", req.Type!, currentAuthority, token, User: new UserResponse
            {
                UserId = user!.Id,
                Name = user.UserName,
                Phone = user.PhoneNumber,
                Avatar =
                    "https://img1.dotnet9.com/site/logo.png"
            });
        }

        return new LoginResponse(false, "error", req.Type!, currentAuthority, token,
            loginResult.Result == SignInResult.LockedOut ? "已被锁定！" : "登录失败，请重试！");
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<bool>?> OutLogin()
    {
        return await Task.FromResult(true);
    }
}