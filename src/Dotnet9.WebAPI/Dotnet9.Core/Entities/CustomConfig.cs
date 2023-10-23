using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;
/// <summary>
/// 自定义配置
/// </summary>
public class CustomConfig : Entity<long>, IAvailability, ICreatedUserId, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 配置名称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Name { get; set; }

    /// <summary>
    /// 配置唯一编码（类名）
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Code { get; set; }

    /// <summary>
    /// 是否是多项（集合）
    /// </summary>
    public bool IsMultiple { get; set; }

    /// <summary>
    /// 配置界面设计（json）
    /// </summary>
    [SugarColumn(ColumnDataType = "text")]
    public string? Json { get; set; }

    /// <summary>
    /// 是否允许创建实体
    /// </summary>
    public bool AllowCreationEntity { get; set; }

    /// <summary>
    /// 是否已生成实体
    /// </summary>
    public bool IsGenerate { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

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
}