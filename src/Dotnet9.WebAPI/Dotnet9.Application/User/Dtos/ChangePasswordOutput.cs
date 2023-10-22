namespace Dotnet9.Application.User.Dtos;

public class ChangePasswordOutput
{
    /// <summary>
    /// 原密码
    /// </summary>
    [Required(ErrorMessage = "原密码不能为空")]
    [StringLength(16, MinimumLength = 6, ErrorMessage = "密码限制6-16个字符")]
    public string OriginalPwd { get; set; }

    /// <summary>
    /// 新密码
    /// </summary>
    [Required(ErrorMessage = "新密码不能为空")]
    [StringLength(16, MinimumLength = 6, ErrorMessage = "密码限制6-16个字符")]
    public string Password { get; set; }
}