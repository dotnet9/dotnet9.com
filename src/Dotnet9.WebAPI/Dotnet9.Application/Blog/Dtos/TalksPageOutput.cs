namespace Dotnet9.Application.Blog.Dtos;

public class TalksPageOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// 图片
    /// </summary>
    public string? Images { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }
    /// <summary>
    /// 是否已点赞
    /// </summary>
    public bool IsPraise { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}