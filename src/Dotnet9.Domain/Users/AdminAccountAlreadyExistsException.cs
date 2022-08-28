namespace Dotnet9.Domain.Users;

public class AdminAccountAlreadyExistsException : Exception
{
    public AdminAccountAlreadyExistsException(string? message) : base(message)
    {
    }
}