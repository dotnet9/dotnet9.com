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
    public async Task<ResponseResult<UserResponse>> Account(LoginRequest req)
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

        if (!loginResult.Result.Succeeded)
        {
            return ResponseResult<UserResponse>.GetError(loginResult.Result == SignInResult.LockedOut
                ? "已被锁定！"
                : "登录失败，请重试！");
        }

        string? token = loginResult.Token;

        if (LoginRequestType.Account != req.Type)
        {
            return ResponseResult<UserResponse>.GetSuccess(new UserResponse { Token = token });
        }

        User? user = await _repository.FindByNameAsync(req.UserName);
        IList<string> roles = await _repository.GetRolesAsync(user!);

        return ResponseResult<UserResponse>.GetSuccess(new UserResponse
        {
            UserId = user!.Id,
            Name = user.UserName,
            Phone = user.PhoneNumber,
            Avatar =
                "https://img1.dotnet9.com/site/logo.png",
            Token = token
        });
    }

    [HttpGet]
    [NoWrapper]
    public async Task<ResponseResult<List<UserMenuItem>>> Menus()
    {
        // TODO the data should be from database, not hard code
        string menuFilePath = "UserMenus.json";
        if (!System.IO.File.Exists(menuFilePath))
        {
            return ResponseResult<List<UserMenuItem>>.GetError("临时菜单文件不存在");
        }

        List<UserMenuItem>? menuItems =
            (await System.IO.File.ReadAllTextAsync(menuFilePath)).ParseJson<List<UserMenuItem>>(true)!;
        return ResponseResult<List<UserMenuItem>>.GetSuccess(menuItems);
    }

    [HttpGet]
    [Authorize]
    public async Task<ResponseResult<DashboardCount>> DashboardCount()
    {
        // TODO 需要实时统计
        List<BlogPostViewCount> weekViewCount = Enumerable.Range(1, 7)
            .Select(index => new BlogPostViewCount($"2023-01-0{index}", Random.Shared.Next(10, 200))).ToList();
        List<BlogPostStatistics> blogPostStatistics = Enumerable.Range(1, 17)
            .Select(index => new BlogPostStatistics($"2023-01-0{index}", Random.Shared.Next(0, 10))).ToList();
        List<AlbumCount> albumCount = new[] { "开源C#", "开源WPF", "开源Winform", "开源MAUI" }
            .Select(album => new AlbumCount(Guid.Empty, album, Random.Shared.Next(10, 1000))).ToList();
        List<CategoryCount> categoryCount = new[] { "C#", "WPF", "Winform", "MAUI" }
            .Select(category => new CategoryCount(Guid.Empty, category, Random.Shared.Next(10, 1000))).ToList();
        List<TagCount> tagCount = new[] { "C#", "开源", "工具", "共享" }
            .Select(tag => new TagCount(Guid.Empty, tag, Random.Shared.Next(10, 1000))).ToList();
        List<BlogPostRank> blogPostRank = new[] { "C#漂亮的控制台", "开源项目集合", "便利工具", "共享社区" }
            .Select(name => new BlogPostRank(name, Random.Shared.Next(10, 1000))).ToList();
        return await Task.FromResult(
            ResponseResult<DashboardCount>.GetSuccess(new DashboardCount(123, 32, 12, 235,
                albumCount,
                categoryCount,
                tagCount,
                blogPostStatistics,
                weekViewCount,
                blogPostRank)));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<bool>?> Logout()
    {
        return await Task.FromResult(true);
    }
}