namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IdDomainService _idService;
    private readonly IIdRepository _repository;

    public LoginController(IdDomainService idService, IIdRepository repository)
    {
        _idService = idService;
        _repository = repository;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateWorld()
    {
        if (await _repository.FindByNameAsync(UserRoleConst.Admin) != null)
        {
            return StatusCode((int)HttpStatusCode.Conflict, "已经初始化过了");
        }

        var user = new User(UserRoleConst.Admin);
        var r = await _repository.CreateAsync(user, "Dotnet9");
        Debug.Assert(r.Succeeded);
        var token = await _repository.GenerateChangedPhoneNumberTokenAsync(user, "18688888888");
        var cr = await _repository.ChangePhoneNumberAsync(user.Id, "18688888888", token);
        Debug.Assert(cr.Succeeded);
        r = await _repository.AddToRolesAsync(user, UserRoleConst.User);
        Debug.Assert(r.Succeeded);
        r = await _repository.AddToRolesAsync(user, UserRoleConst.Admin);
        Debug.Assert(r.Succeeded);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetUserInfo()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _repository.FindByIdAsync(Guid.Parse(userId!));
        if (user == null)
        {
            return NotFound();
        }

        return new UserResponse(user.Id, user.PhoneNumber!, user.CreationTime);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<IdentityResult>> ChangePassword(ChangeMyPasswordRequest req)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _repository.FindByIdAsync(Guid.Parse(userId!));
        if (user == null)
        {
            return NotFound();
        }

        var checkResult = await _repository.CheckForSignInAsync(user, req.Password, false);
        if (!checkResult.Succeeded)
        {
            return checkResult.IsLockedOut
                ? StatusCode((int)HttpStatusCode.Locked, "用户已经被锁定")
                : BadRequest($"修改失败：{checkResult}");
        }

        var changeResult = await _repository.ChangePasswordAsync(user.Id, req.Password2);
        if (changeResult.Succeeded)
        {
            return changeResult;
        }

        return BadRequest("修改失败");
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string?>> LoginByPhoneAndPwd(LoginByPhoneAndPwdRequest req)
    {
        var (checkResult, token) = await _idService.LoginByPhoneAndPwdAsync(req.PhoneNumber, req.Password);
        if (checkResult.Succeeded)
        {
            return token;
        }

        return checkResult.IsLockedOut
            ? StatusCode((int)HttpStatusCode.Locked, "用户已经被锁定")
            : BadRequest($"登录失败：{checkResult}");
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string?>> LoginByUserNameAndPwd(
        LoginByUserNameAndPwdRequest req)
    {
        var (checkResult, token) = await _idService.LoginByUserNameAndPwdAsync(req.UserName, req.Password);
        if (checkResult.Succeeded)
        {
            return token;
        }

        return checkResult.IsLockedOut
            ? StatusCode((int)HttpStatusCode.Locked, "用户已经被锁定")
            : BadRequest($"登录失败：{checkResult}");
    }
}