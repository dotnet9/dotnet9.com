namespace Dotnet9.Application.Client.Dtos;

public class ArticleOutput
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
    /// 分类ID
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 分类别名
    /// </summary>
    public string CategorySlug { get; set; }

    /// <summary>
    /// 专辑ID
    /// </summary>
    public long AlbumId { get; set; }

    /// <summary>
    /// 专辑名称
    /// </summary>
    public string AlbumName { get; set; }

    /// <summary>
    /// 专辑别名
    /// </summary>
    public string AlbumSlug { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 创作类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public CreationType CreationType { get; set; }

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
    /// 标签
    /// </summary>
    public List<TagsOutput> Tags { get; set; }
}