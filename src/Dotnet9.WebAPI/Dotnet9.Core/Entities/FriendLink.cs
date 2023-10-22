using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

/// <summary>
/// 友情链接
/// </summary>
public class FriendLink : Entity<long>, ISortable, IAvailability, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 博客用户Id
    /// </summary>
    public long? AppUserId { get; set; }

    /// <summary>
    /// 网站名称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string SiteName { get; set; }

    /// <summary>
    /// 网站链接
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Link { get; set; }

    /// <summary>
    /// 网站logo
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Logo { get; set; }

    /// <summary>
    /// 对方博客友链的地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Url { get; set; }

    /// <summary>
    /// 是否忽略对方站点是否存在本站链接
    /// </summary>
    public bool IsIgnoreCheck { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    public int Sort { get; set; }

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