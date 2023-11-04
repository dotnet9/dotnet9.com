namespace Dotnet9.Application.Blog.Dtos;

public class AddAlbumInput
{
    /// <summary>
    /// 专辑名称
    /// </summary>
    [Required(ErrorMessage = "专辑名称为必填项")]
    [MaxLength(32, ErrorMessage = "专辑名限制32个字符内")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 专辑别名
    /// </summary>
    [Required(ErrorMessage = "专辑别名为必填项")]
    [MaxLength(128, ErrorMessage = "专辑别名制128个字符内")]
    public string Slug { get; set; } = null!;

    /// <summary>
    /// 封面图
    /// </summary>
    [MaxLength(256)]
    [Required(ErrorMessage = "请上传封面图")]
    public string Cover { get; set; } = null!;

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    [Required(ErrorMessage = "排序值为必填项")]
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(200, ErrorMessage = "备注限制200个字符内")]
    public string? Remark { get; set; }
}