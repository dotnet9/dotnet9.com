namespace Dotnet9.Core.Entities;
/// <summary>
/// 文章所属栏目表
/// </summary>
public class ArticleCategory : Entity<long>
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 栏目Id
    /// </summary>
    public long CategoryId { get; set; }
}