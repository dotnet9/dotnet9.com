namespace Dotnet9.Application.Blog.Dtos;

public class AddFriendLinkInput
{
    /// <summary>
    /// 网站名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "站点名称限制32个字符内")]
    [Required(ErrorMessage = "友链站点名称为必填项")]
    public string SiteName { get; set; } = null!;

    /// <summary>
    /// 网站链接
    /// </summary>
    [MaxLength(256, ErrorMessage = "网站链接限制256个字符内")]
    [Required(ErrorMessage = "网站链接为必填项")]
    public string Link { get; set; } = null!;

    /// <summary>
    /// 网站logo
    /// </summary>
    [MaxLength(256, ErrorMessage = "网站Logo限制256个字符")]
    [Required(ErrorMessage = "网站Logo为必填项")]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// 对方博客友链的地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "对方博客友链限制256个字符")]
    public string Url { get; set; } = null!;

    /// <summary>
    /// 是否忽略对方站点是否存在本站链接
    /// </summary>
    public bool IsIgnoreCheck { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(200, ErrorMessage = "描述限制200字符")]
    public string Remark { get; set; } = null!;

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    [Required(ErrorMessage = "排序为必填项")]
    public int Sort { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
}