namespace Dotnet9.Application.Client.Dtos;

public class ArticleInfoOutput
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 别名
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    public string Cover { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime PublishTime { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 浏览量
    /// </summary>
    public int Views { get; set; }

    /// <summary>
    /// 转载类型
    /// </summary>
    public CreationType CreationType { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }

    /// <summary>
    /// 是否是html
    /// </summary>
    public bool IsHtml { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 转载链接
    /// </summary>
    public string Link { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    public int PraiseTotal { get; set; }

    /// <summary>
    /// 当前用户是否已点赞
    /// </summary>
    public bool IsPraise { get; set; }

    /// <summary>
    /// 分类Id
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 专辑Id
    /// </summary>
    public long AlbumId { get; set; }

    /// <summary>
    /// 专辑名称
    /// </summary>
    public string AlbumName { get; set; }

    /// <summary>
    /// 最后更新时间
    /// </summary>
    public DateTime? UpdatedTime { get; set; }

    /// <summary>
    /// 上一篇
    /// </summary>
    public ArticleBasicsOutput Prev { get; set; }

    /// <summary>
    /// 下一篇
    /// </summary>
    public ArticleBasicsOutput Next { get; set; }

    /// <summary>
    /// 随机
    /// </summary>
    public List<ArticleBasicsOutput> Random { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public List<TagsOutput> Tags { get; set; }
}