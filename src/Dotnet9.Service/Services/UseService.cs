namespace Dotnet9.Service.Services;

public class UserService : ServiceBase
{
    public UserService() : base("/api/user")
    {
    }

    [AllowAnonymous]
    public async Task<UserDto?> PostLoginByAccountAsync(IEventBus eventBus, [FromBody] LoginByAccountDto model)
    {
        var command = new LoginByAccountCommand(model);
        await eventBus.PublishAsync(command);
        return command.Result?.Map<UserDto>();
    }
}