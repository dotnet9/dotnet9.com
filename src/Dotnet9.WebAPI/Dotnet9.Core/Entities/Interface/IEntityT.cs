namespace Dotnet9.Core.Entities;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IEntity<TKey> : IEntity
{
    /// <summary>
    /// 
    /// </summary>
    TKey Id { get; set; }
}