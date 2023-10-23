using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;
/// <summary>
/// 自定义配置子项
/// </summary>
public class CustomConfigItem : Entity<long>, IAvailability, ICreatedUserId, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 自定义配置Id
    /// </summary>
    public long ConfigId { get; set; }

    /// <summary>
    /// 配置
    /// </summary>
    [SugarColumn(ColumnDataType = "text")]
    public string Json { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

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