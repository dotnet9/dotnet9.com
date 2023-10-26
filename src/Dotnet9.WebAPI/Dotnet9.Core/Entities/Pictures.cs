namespace Dotnet9.Core.Entities;

/// <summary>
/// 模块封面图片表
/// </summary>
public class Pictures : Entity<long>, ICreatedTime
{
    /// <summary>
    /// 封面Id
    /// </summary>
    public long CoverId { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Url { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}