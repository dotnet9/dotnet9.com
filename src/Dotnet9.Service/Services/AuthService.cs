namespace Dotnet9.Service.Services;

public class AuthService : ServiceBase
{
    public AuthService() : base("/api/auth")
    {
    }

    [AllowAnonymous]
    public async Task<ResponseResult<UserDto?>> PostLoginByAccountAsync(IEventBus eventBus,
        [FromBody] LoginByAccountDto model)
    {
        var command = new LoginByAccountCommand(model);
        await eventBus.PublishAsync(command);
        return command.Result;
    }
}