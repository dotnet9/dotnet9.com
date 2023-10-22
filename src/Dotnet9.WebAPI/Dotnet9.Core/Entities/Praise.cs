namespace Dotnet9.Core.Entities;
/// <summary>
/// 点赞表
/// </summary>
public class Praise : Entity<long>, ICreatedTime
{
    /// <summary>
    /// 用户ID 
    /// </summary>
    public long AccountId { get; set; }

    /// <summary>
    /// 点赞对象ID
    /// </summary>
    public long ObjectId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}