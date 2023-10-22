namespace Dotnet9.Application.Blog.Dtos;

public class AddTagInput
{
    /// <summary>
    /// 标签名称
    /// </summary>
    [Required(ErrorMessage = "标签名称为必填项")]
    [MaxLength(32, ErrorMessage = "标签名限制32个字符内")]
    public string Name { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    [MaxLength(256)]
    [Required(ErrorMessage = "请上传封面图")]
    public string Cover { get; set; }

    /// <summary>
    /// 标签颜色
    /// </summary>
    [MaxLength(64)]
    public string Color { get; set; }

    /// <summary>
    /// 标签图标
    /// </summary>
    [MaxLength(32)]
    public string Icon { get; set; }

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
    public string Remark { get; set; }
}