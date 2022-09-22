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
    public async Task<GetUserListResponse> GetAllUsers(GetUserListRequest request)
    {
        var userWithRoles = new List<UserDto>();
        foreach (var user in _userManager.Users.ToList())
        {
            var roleNames = await _userManager.GetRolesAsync(user);
            userWithRoles.Add(new UserDto(user.Id, user.UserName!, roleNames.ToArray(), user.PhoneNumber!,
                user.CreationTime));
        }

        return new GetUserListResponse(userWithRoles.ToArray(), true, request.PageSize, request.Current);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDto?> FindById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return null;
        }

        var roleNames = await _userManager.GetRolesAsync(user);
        return new UserDto(user.Id, user.UserName!, roleNames.ToArray(), user.PhoneNumber!, user.CreationTime);
    }

    [HttpPost]
    public async Task<ActionResult> AddUser(AddUserRequest req)
    {
        var (result, user, password) = await _repository.AddUserAsync(req.UserName, req.RoleName, req.PhoneNumber);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }

        var userCreatedEvent = new UserCreatedEvent(user!.Id, req.UserName, password!, req.PhoneNumber);
        _eventBus.Publish("Dotnet9.WebAPI.User.Created", userCreatedEvent);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == id.ToString())
        {
            return BadRequest("管理员不能删除自己");
        }

        await _repository.RemoveUserAsync(id);
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateUser(Guid id, EditUserRequest req)
    {
        var user = await _repository.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("用户没找到");
        }

        user.PhoneNumber = req.PhoneNumber;
        await _userManager.UpdateAsync(user);
        if (req.RoleName == UserRoleConst.User && await _userManager.IsInRoleAsync(user, UserRoleConst.Admin))
        {
            await _userManager.RemoveFromRoleAsync(user, UserRoleConst.Admin);
        }
        else if (req.RoleName == UserRoleConst.Admin &&
                 await _userManager.IsInRoleAsync(user, UserRoleConst.Admin) == false)
        {
            await _userManager.AddToRoleAsync(user, UserRoleConst.Admin);
        }

        return Ok();
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult> ResetUserPassword(Guid id)
    {
        var (result, user, password) = await _repository.ResetPasswordAsync(id);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }

        var eventData = new ResetPasswordEvent(user!.Id, user.UserName!, password!, user.PhoneNumber!);
        _eventBus.Publish("Dotnet9.WebAPI.User.PasswordReset", eventData);
        return Ok();
    }
}