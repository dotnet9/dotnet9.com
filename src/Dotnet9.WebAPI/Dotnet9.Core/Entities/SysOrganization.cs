using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

/// <summary>
/// 组织机构信息表
/// </summary>
public class SysOrganization : Entity<long>, IAvailability, ISortable, ICreatedUserId, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 父级Id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 部门编码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = null!;

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

    /// <summary>
    /// 子部门
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<SysOrganization> Children { get; set; } = new();
}