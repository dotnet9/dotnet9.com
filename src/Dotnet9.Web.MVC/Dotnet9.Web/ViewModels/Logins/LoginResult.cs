namespace Dotnet9.Web.ViewModels.Logins;

public class LoginResult
{
    public string? Token { get; set; }

    public DateTime Expiration { get; set; }
}