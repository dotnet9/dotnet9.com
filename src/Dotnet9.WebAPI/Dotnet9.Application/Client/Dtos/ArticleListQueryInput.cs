namespace Dotnet9.Application.Client.Dtos;

public class ArticleListQueryInput : Pagination
{
    /// <summary>
    /// 标签ID
    /// </summary>
    public long? TagId { get; set; }
    /// <summary>
    /// 分类ID
    /// </summary>
    public long? CategoryId { get; set; }

    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; }
}