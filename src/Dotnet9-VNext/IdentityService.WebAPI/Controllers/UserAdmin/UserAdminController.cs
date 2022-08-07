namespace IdentityService.WebAPI.Controllers.UserAdmin;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = "Admin")]
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
    public Task<UserDto[]> FindAllUsers()
    {
        return _userManager.Users.Select(u => UserDto.Create(u)).ToArrayAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDto> FindById(Guid id)
    {
        User? user = await _userManager.FindByIdAsync(id.ToString());
        return UserDto.Create(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> AddAdminUser(AddAdminUserRequest req)
    {
        (IdentityResult result, User? user, string? password) =
            await _repository.AddAdminUserAsync(req.UserName, req.PhoneNumber);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }

        UserCreatedEvent userCreatedEvent = new(user.Id, req.UserName, password, req.PhoneNumber);
        _eventBus.Publish("IdentityService.User.Created", userCreatedEvent);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAdminUser(Guid id)
    {
        await _repository.RemoveUserAsync(id);
        return Ok();
    }

    [HttpPut]
    [Route("{id")]
    public async Task<ActionResult> UpdateAdminUser(Guid id, EditAdminUserRequest req)
    {
        User? user = await _repository.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("用户没找到");
        }

        user.PhoneNumber = req.PhoneNumber;
        await _userManager.UpdateAsync(user);
        return Ok();
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult> ResetAdminUserPassword(Guid id)
    {
        (IdentityResult result, User? user, string? password) =
            await _repository.ResetPasswordAsync(id);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }

        ResetPasswordEvent eventData = new(user!.Id, user!.UserName, password, user.PhoneNumber);
        _eventBus.Publish("IdentityService.User.PasswordReset", eventData);
        return Ok();
    }
}