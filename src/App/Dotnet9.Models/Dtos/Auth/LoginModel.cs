using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Models.Dtos.Auth;

public class LoginModel
{
    /// <summary>
    ///     账号
    /// </summary>
    [Required]
    public string UserName { get; set; }

    /// <summary>
    ///     密码
    /// </summary>
    [Required]
    public string Pwd { get; set; }

    /// <summary>
    ///     验证码
    /// </summary>
    [Required]
    public string ValidCode { get; set; }
}

public class LoginResp
{
    public string Token { get; set; }
}