using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

/// <summary>
/// 角色表
/// </summary>
public class SysRole : Entity<long>, IAvailability, ISortable, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 角色名
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Name { get; set; }

    /// <summary>
    /// 角色编码
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Code { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}