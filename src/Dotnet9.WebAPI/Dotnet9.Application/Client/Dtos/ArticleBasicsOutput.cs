namespace Dotnet9.Application.Client.Dtos;

public class ArticleBasicsOutput
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 封面图
    /// </summary>
    public string Cover { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }
}