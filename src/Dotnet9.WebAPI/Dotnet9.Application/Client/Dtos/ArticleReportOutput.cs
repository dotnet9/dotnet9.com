namespace Dotnet9.Application.Client.Dtos;

public class ArticleReportOutput
{
    /// <summary>
    /// 文章数量
    /// </summary>
    public int ArticleCount { get; set; }

    /// <summary>
    /// 标签数量
    /// </summary>
    public int TagCount { get; set; }

    /// <summary>
    /// 分类数量
    /// </summary>
    public int CategoryCount { get; set; }

    /// <summary>
    /// 专辑数量
    /// </summary>
    public int AlbumCount { get; set; }

    /// <summary>
    /// 用户量
    /// </summary>
    public int UserCount { get; set; }

    /// <summary>
    /// 友链数量
    /// </summary>
    public int LinkCount { get; set; }
}