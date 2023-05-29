namespace Dotnet9.Service.Application.Auth.Commands;

public record LoginByAccountCommand(LoginByAccountDto Model) : Command
{
    public UserDto? Result { get; set; }
}