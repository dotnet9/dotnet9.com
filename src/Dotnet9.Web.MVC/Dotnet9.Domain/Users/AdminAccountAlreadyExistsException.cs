namespace Dotnet9.Domain.Users;

public class AdminAccountAlreadyExistsException : System.Exception
{
    public AdminAccountAlreadyExistsException(string? message) : base(message)
    {

    }
}