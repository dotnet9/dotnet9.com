namespace Dotnet9.Application.Blog.Dtos;

public class ArticlePageQueryInput : Pagination
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 栏目ID
    /// </summary>
    public long? CategoryId { get; set; }
}