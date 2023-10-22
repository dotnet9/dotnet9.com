namespace Dotnet9.Core.Entities;
/// <summary>
/// 评论表
/// </summary>
public class Comments : Entity<long>, ISoftDelete, ICreatedTime
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
    /// 当前评论人ID
    /// </summary>
    public long AccountId { get; set; }

    /// <summary>
    /// 回复人ID
    /// </summary>
    public long? ReplyAccountId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [SugarColumn(Length = 1024)]
    public string Content { get; set; }

    /// <summary>
    /// Ip地址
    /// </summary>
    [SugarColumn(Length = 128)]
    public string IP { get; set; }

    /// <summary>
    /// IP所属地
    /// </summary>
    [SugarColumn(Length = 128)]
    public string Geolocation { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}