namespace Dotnet9.Core.Entities;

/// <summary>
/// 排序
/// </summary>
public interface ISortable
{
    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    int Sort { get; set; }
}