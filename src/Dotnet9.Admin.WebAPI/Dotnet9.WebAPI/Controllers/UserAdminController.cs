namespace Dotnet9.WebAPI.Controllers;

[Route("api/user")]
[ApiController]
[Authorize(Roles = UserRoleConst.Admin)]
public class UserAdminController : ControllerBase
{
    private readonly IEventBus _eventBus;
    private readonly IIdRepository _repository;
    private readonly IdUserManager _userManager;

    public UserAdminController(IdUserManager userManager, IEventBus eventBus, IIdRepository repository)
    {
        _userManager = userManager;
        _eventBus = eventBus;
        _repository = repository;
    }

    [HttpGet]
    [NoWrapper]
    public async Task<GetUserListResponse> GetAllUsers([FromQuery] GetUserListRequest request)
    {
        List<UserDto> userWithRoles = new();
        IQueryable<User> users = _userManager.Users.AsQueryable().AsNoTracking();
        if (request.UserName != null)
        {
            users = users.Where(x =>
                EF.Functions.Like(x.UserName!.ToLower(), $"%{request.UserName!.ToLower()}%"));
        }

        if (request.PhoneNumber != null)
        {
            users = users.Where(x =>
                EF.Functions.Like(x.PhoneNumber!.ToLower(), $"%{request.PhoneNumber!.ToLower()}%"));
        }

        foreach (User user in users.ToList())
        {
            IList<string>? roleNames = await _userManager.GetRolesAsync(user);
            userWithRoles.Add(new UserDto(user.Id, user.UserName!, roleNames.JoinAsString(","), user.PhoneNumber!,
                user.CreationTime));
        }

        return new GetUserListResponse(userWithRoles.ToArray(), true, request.PageSize, request.Current);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDto?> FindById(Guid id)
    {
        User? user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return null;
        }

        IList<string>? roleNames = await _userManager.GetRolesAsync(user);
        return new UserDto(user.Id, user.UserName!, roleNames.JoinAsString(","), user.PhoneNumber!, user.CreationTime);
    }

    [HttpPost]
    public async Task<ActionResult<AddUserResponse>> AddUser([FromBody] AddUserRequest req)
    {
        (IdentityResult result, User? user, string? password) =
            await _repository.AddUserAsync(req.UserName, req.RoleNames, req.PhoneNumber);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }

        UserCreatedEvent userCreatedEvent = new(user!.Id, req.UserName, password!, req.PhoneNumber);
        _eventBus.Publish("Dotnet9.WebAPI.User.Created", userCreatedEvent);
        return new AddUserResponse(req.UserName, password!);
    }

    [HttpDelete]
    public async Task<ResponseResult<bool>> DeleteUser([FromBody] DeleteUserRequest request)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        if (request.Ids.Contains(Guid.Parse(userId)))
        {
            return ResponseResult<bool>.GetError("不能删除自己");
        }

        foreach (Guid id in request.Ids)
        {
            await _repository.RemoveUserAsync(id);
        }

        return true;
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] EditUserRequest req)
    {
        User? user = await _repository.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("用户没找到");
        }

        user.PhoneNumber = req.PhoneNumber;
        await _userManager.UpdateAsync(user);
        if (req.RoleNames == UserRoleConst.User && await _userManager.IsInRoleAsync(user, UserRoleConst.Admin))
        {
            await _userManager.RemoveFromRoleAsync(user, UserRoleConst.Admin);
        }
        else if (req.RoleNames == UserRoleConst.Admin &&
                 await _userManager.IsInRoleAsync(user, UserRoleConst.Admin) == false)
        {
            await _userManager.AddToRoleAsync(user, UserRoleConst.Admin);
        }

        return Ok();
    }

    [HttpPut]
    [Route("/api/user/{id}/updatebase")]
    public async Task<ActionResult> UpdateUserBase(Guid id, [FromBody] EditUserBaseRequest req)
    {
        User? user = await _repository.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("用户没找到");
        }

        user.NickName = req.NickName;
        user.Brief = req.Brief;
        user.WebSite = req.WebSite;
        await _userManager.UpdateAsync(user);

        return Ok(req);
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult<ResetPasswordResponse>> ResetUserPassword(Guid id)
    {
        (IdentityResult result, User? user, string? password) = await _repository.ResetPasswordAsync(id);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }

        ResetPasswordEvent eventData = new(user!.Id, user.UserName!, password!, user.PhoneNumber!);
        _eventBus.Publish("Dotnet9.WebAPI.User.PasswordReset", eventData);
        return new ResetPasswordResponse(user.UserName!, password!);
    }

    [HttpGet]
    [Route("/api/user/area")]
    public async Task<ResponseResult<List<UserArea>>> Area(int type)
    {
        // TODO 需要实时统计
        List<UserArea> areaCount = new[] { "北京", "上海", "广州", "深圳", "四川", "香港" }
            .Select(name => new UserArea(name, Random.Shared.Next(10, 200))).ToList();
        return await Task.FromResult(ResponseResult<List<UserArea>>.GetSuccess(areaCount));
    }
}