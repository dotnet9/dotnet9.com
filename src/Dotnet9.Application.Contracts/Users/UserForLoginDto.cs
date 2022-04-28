namespace Dotnet9.Application.Contracts.Users;

public class UserForLoginDto
{
    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;
}