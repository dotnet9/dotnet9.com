namespace Dotnet9.Application.Client.Dtos;

public class AddLinkOutput
{
    /// <summary>
    /// 网站名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "网站名称限制32个字符内")]
    [Required(ErrorMessage = "网站名称为必填项")]
    public string SiteName { get; set; } = null!;

    /// <summary>
    /// 网站链接
    /// </summary>
    [MaxLength(256, ErrorMessage = "网站链接域名限制256个字符内")]
    [Required(ErrorMessage = "网站链接为必填项")]
    [DataValidation(ValidationTypes.Url, ErrorMessage = "链接不符合规范")]
    public string Link { get; set; } = null!;

    /// <summary>
    /// 网站logo
    /// </summary>
    [MaxLength(256, ErrorMessage = "Logo限制256个字符内")]
    [DataValidation(ValidationTypes.Url, ErrorMessage = "链接不符合规范")]
    [Required(ErrorMessage = "请输入您的Logo地址")]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// 对方博客友链的地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "友链页面地址限制256个字符内")]
    [DataValidation(ValidationTypes.Url, ErrorMessage = "链接不符合规范")]
    [Required(ErrorMessage = "请输入您站点的友链页面地址")]
    public string Url { get; set; } = null!;

    /// <summary>
    /// 网站介绍
    /// </summary>
    [MaxLength(200, ErrorMessage = "网站介绍限制200个字符内")]
    [Required(ErrorMessage = "网站介绍为必填项")]
    public string Remark { get; set; } = null!;
}