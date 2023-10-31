namespace Dotnet9.Application.Client.Dtos;

public class ArticleListQueryInput : Pagination
{
    /// <summary>
    /// 标签名
    /// </summary>
    public string? TagName { get; set; }

    /// <summary>
    /// 分类别名
    /// </summary>
    public string? CategorySlug { get; set; }

    /// <summary>
    /// 专辑ID
    /// </summary>
    public string? AlbumSlug { get; set; }

    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; }
}