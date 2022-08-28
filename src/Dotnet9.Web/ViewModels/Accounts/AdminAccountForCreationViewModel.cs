namespace Dotnet9.Web.ViewModels.Accounts;

public class AdminAccountForCreationViewModel
{
    [Display(Name = "账号")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(3, ErrorMessage = "{0}长度不能少于3个字符")]
    [MaxLength(10, ErrorMessage = "{0}长度不能超过10个字符")]
    public string Account { get; set; } = null!;

    [Display(Name = "邮箱")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(5, ErrorMessage = "{0}长度不能少于5个字符")]
    [MaxLength(20, ErrorMessage = "{0}长度不能超过20个字符")]
    [EmailAddress(ErrorMessage = "{0}格式不正确")]
    public string Email { get; set; } = null!;

    [Display(Name = "密码")]
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(6, ErrorMessage = "{0}长度不能少于6个字符")]
    [MaxLength(15, ErrorMessage = "{0}长度不能超过15个字符")]
    public string Password { get; set; } = null!;
}