using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

public class Talks : Entity<long>, ISoftDelete, ICreatedTime, IAvailability
{
    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 说说正文
    /// </summary>
    [SugarColumn(ColumnDataType = "text")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// 图片
    /// </summary>
    [SugarColumn(Length = 2048)]
    public string? Images { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}