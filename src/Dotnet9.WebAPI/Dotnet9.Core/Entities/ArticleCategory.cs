namespace Dotnet9.Core.Entities;
/// <summary>
/// 文章所属分类表
/// </summary>
public class ArticleCategory : Entity<long>
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 分类Id
    /// </summary>
    public long CategoryId { get; set; }
}