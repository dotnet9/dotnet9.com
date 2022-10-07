namespace Dotnet9.Application.Contracts.Users;

public class UserForCreationDto
{
    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
}