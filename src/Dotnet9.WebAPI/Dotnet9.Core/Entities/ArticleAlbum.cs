namespace Dotnet9.Core.Entities;

/// <summary>
/// 文章所属专辑表
/// </summary>
public class ArticleAlbum : Entity<long>
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 专辑Id
    /// </summary>
    public long AlbumId { get; set; }
}