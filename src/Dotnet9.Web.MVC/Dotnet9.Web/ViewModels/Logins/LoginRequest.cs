namespace Dotnet9.Web.ViewModels.Logins;

public class LoginRequest
{
    [Required(ErrorMessage = "用户名是必填项")] public string? UserName { get; set; }

    [Required(ErrorMessage = "密码是必填项")] public string? Password { get; set; }
}