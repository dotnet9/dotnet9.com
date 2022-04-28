namespace Dotnet9.AdminAPI.ViewModels.Accounts;

public class UserException : Exception
{
    public UserException(string? message) : base(message)
    {
    }
}