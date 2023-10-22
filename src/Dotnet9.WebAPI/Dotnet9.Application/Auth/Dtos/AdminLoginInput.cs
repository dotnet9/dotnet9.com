namespace Dotnet9.Application.Auth.Dtos;

public class AdminLoginInput
{
    /// <summary>
    /// 登录名
    /// </summary>
    [Required(ErrorMessage = "请输入登录名")]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "请输入登录密码")]
    public string Password { get; set; }

    /// <summary>
    /// 验证码唯一ID
    /// </summary>
    [Required(ErrorMessage = "缺少参数")]
    public string Id { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    [Required(ErrorMessage = "请输入验证码")]
    public string Code { get; set; }
}