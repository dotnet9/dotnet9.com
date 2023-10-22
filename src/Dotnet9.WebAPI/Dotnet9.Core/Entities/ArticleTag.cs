namespace Dotnet9.Core.Entities;
/// <summary>
/// 文章标签表
/// </summary>
public class ArticleTag : Entity<long>
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 标签Id
    /// </summary>
    public long TagId { get; set; }
}