namespace Dotnet9.Application.Blog.Dtos;

public class AddTalksInput
{
    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 说说正文
    /// </summary>
    [Required(ErrorMessage = "说说正文为必填项")]
    public string Content { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    [MaxLength(2048)]
    public string Images { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
}