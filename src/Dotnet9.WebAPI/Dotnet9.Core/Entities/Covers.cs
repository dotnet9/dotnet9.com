using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

/// <summary>
/// 模块封面表
/// </summary>
public class Covers : Entity<long>, IAvailability, ISortable, ICreatedUserId, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 模块名称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 封面图
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Cover { get; set; } = null!;

    /// <summary>
    /// 模块类型
    /// </summary>
    public CoverType? Type { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsVisible { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public long CreatedUserId { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}