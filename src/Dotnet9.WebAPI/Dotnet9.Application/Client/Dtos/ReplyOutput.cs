namespace Dotnet9.Application.Client.Dtos;

public class ReplyOutput
{
    /// <summary>
    /// 评论ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = null!;
    /// <summary>
    /// 博主标识
    /// </summary>
    public bool IsBlogger { get; set; }
    /// <summary>
    /// 顶级楼层评论ID
    /// </summary>
    public long? RootId { get; set; }
    /// <summary>
    /// 上级评论ID
    /// </summary>
    public long? ParentId { get; set; }
    /// <summary>
    /// 当前评论人ID
    /// </summary>
    public long AccountId { get; set; }
    /// <summary>
    /// 回复人ID
    /// </summary>
    public long? ReplyAccountId { get; set; }
    /// <summary>
    /// 当前人昵称
    /// </summary>
    public string NickName { get; set; } = null!;
    /// <summary>
    /// 回复人昵称
    /// </summary>
    public string RelyNickName { get; set; } = null!;
    /// <summary>
    /// 当前评论人头像
    /// </summary>
    public string? Avatar { get; set; }
    /// <summary>
    /// Ip地址
    /// </summary>
    public string? IP { get; set; }
    /// <summary>
    /// 点赞数量
    /// </summary>
    public int PraiseTotal { get; set; }
    /// <summary>
    /// 是否已点赞
    /// </summary>
    public bool IsPraise { get; set; }
    /// <summary>
    /// Ip所属地
    /// </summary>
    public string? Geolocation { get; set; }
    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}