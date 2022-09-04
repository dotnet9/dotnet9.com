using Dotnet9.EventBus;
using Dotnet9.WebAPI.Application.Contracts.UserAdmin;
using Dotnet9.WebAPI.Events;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
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
    public Task<UserDTO[]> GetAllUsers()
    {
        return _userManager.Users.Select(u => new UserDTO(u.Id, u.UserName, u.PhoneNumber, u.CreationTime))
            .ToArrayAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDTO> FindById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return new UserDTO(user.Id, user.UserName, user.PhoneNumber, user.CreationTime);
    }

    public async Task<ActionResult> AddAdminUser(AddAdminUserRequest req)
    {
        var (result, user, password) = await _repository.AddAdminUserAsync(req.UserName, req.PhoneNumber);
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
    public async Task<ActionResult> DeleteAdminUser(Guid id)
    {
        await _repository.RemoveUserAsync(id);
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateAdminUser(Guid id, EditAdminUserRequest req)
    {
        var user = await _repository.FindByIdAsync(id);
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
        var (result, user, password) = await _repository.ResetPasswordAsync(id);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }

        var eventData = new ResetPasswordEvent(user!.Id, user.UserName, password!, user.PhoneNumber);
        _eventBus.Publish("Dotnet9.WebAPI.User.PasswordReset", eventData);
        return Ok();
    }
}