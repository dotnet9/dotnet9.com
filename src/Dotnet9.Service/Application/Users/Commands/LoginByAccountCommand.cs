namespace Dotnet9.Service.Application.Users.Commands;

public record LoginByAccountCommand(LoginByAccountDto Model) : Command
{
    public UserDto? Result { get; set; }
}