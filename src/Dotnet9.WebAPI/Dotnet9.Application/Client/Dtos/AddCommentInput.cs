namespace Dotnet9.Application.Client.Dtos;

public class AddCommentInput
{
    /// <summary>
    ///  对应模块ID（null表留言，0代表友链的评论）
    /// </summary>
    public long? ModuleId { get; set; }

    /// <summary>
    /// 顶级楼层评论ID
    /// </summary>
    public long? RootId { get; set; }

    /// <summary>
    /// 被回复的评论ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 回复人ID
    /// </summary>
    public long? ReplyAccountId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [MaxLength(600, ErrorMessage = "内容限制600个字符内")]
    [Required(ErrorMessage = "请输入内容")]
    public string Content { get; set; } = null!;
}