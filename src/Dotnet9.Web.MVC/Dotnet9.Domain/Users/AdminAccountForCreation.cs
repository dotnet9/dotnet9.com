namespace Dotnet9.Domain.Users;

public class AdminAccountForCreation
{
    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
}