namespace Dotnet9.Application.Blog.Dtos;

public class ArticlePageQueryInput : Pagination
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    public long? CategoryId { get; set; }
}