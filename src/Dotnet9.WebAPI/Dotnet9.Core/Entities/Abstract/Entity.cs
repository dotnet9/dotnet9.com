namespace Dotnet9.Core.Entities;

/// <summary>
/// 表实体继承
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class Entity<TKey> : IEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键")]
    public virtual TKey Id { get; set; }
}