namespace Dotnet9.Models.Data.Blogs;

public class Posts : BaseEntity<Guid>
{
    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 别名
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// 根据别名生成的短Id
    /// </summary>
    public string ShortId { get; set; }

    /// <summary>
    ///     缩略图
    /// </summary>
    public string Thumb { get; set; }

    /// <summary>
    ///     内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     摘要
    /// </summary>
    public string Snippet { get; set; }

    /// <summary>
    ///     阅读次数
    /// </summary>
    public int ReadCount { get; set; }

    /// <summary>
    ///     标签
    /// </summary>
    public List<PostTagRelation> TagRelations { get; set; } = new();

    /// <summary>
    ///     分类
    /// </summary>
    public List<PostCateRelation> CateRelations { get; set; } = new();

    /// <summary>
    ///     评论
    /// </summary>
    public List<PostComments> PostComments { get; set; } = new();

    /// <summary>
    ///     是否发布
    /// </summary>
    public bool IsPublish { get; set; }

    /// <summary>
    ///     是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    ///     访问记录
    /// </summary>
    public List<PostVisitRecord> VisitRecords { get; set; } = new();


    public bool IsDelete { get; set; }


    public DateTime UpdateTime { get; set; }
}