namespace Dotnet9.Web.ViewModels.Accounts;

public class UserException : Exception
{
    public UserException(string? message) : base(message)
    {
    }
}