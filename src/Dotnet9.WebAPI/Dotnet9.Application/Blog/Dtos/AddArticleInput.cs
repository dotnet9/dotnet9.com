namespace Dotnet9.Application.Blog.Dtos;

public class AddArticleInput
{
    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage = "标题为必填项")]
    [MaxLength(128, ErrorMessage = "标题限制128个字符内")]
    public string Title { get; set; }

    /// <summary>
    /// 概要
    /// </summary>
    [Required(ErrorMessage = "概要为必填项")]
    [MaxLength(256, ErrorMessage = "概要限制256个字符内")]
    public string Summary { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    [Required(ErrorMessage = "上传封面图")]
    [MaxLength(256)]
    public string Cover { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [Required(ErrorMessage = "文章作者为必填项")]
    [MaxLength(32, ErrorMessage = "文章作者限制32个字符内")]
    public string Author { get; set; }

    /// <summary>
    /// 原文地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "原文地址限制256个字符内")]
    [DataValidation(ValidationTypes.Url, ErrorMessage = "请输入正确的网络链接", AllowNullValue = true)]
    public string Link { get; set; }

    /// <summary>
    /// 创作类型
    /// </summary>
    public CreationType CreationType { get; set; }

    /// <summary>
    /// 文章正文（Html或markdown）
    /// </summary>
    [Required(ErrorMessage = "文章内容为必填项")]
    public string Content { get; set; }

    /// <summary>
    /// 文章正文是否为html代码
    /// </summary>
    public bool IsHtml { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime PublishTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    [Required(ErrorMessage = "排序值为必填项")]
    public int Sort { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }

    /// <summary>
    /// 过期时间（过期后文章不显示）
    /// </summary>
    public DateTime? ExpiredTime { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    [Required(ErrorMessage = "请选择文章标签")]
    public List<long> Tags { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    [Required(ErrorMessage = "请选择文章分类")]
    public long CategoryId { get; set; }

    /// <summary>
    /// 专辑ID
    /// </summary>
    [Required(ErrorMessage = "请选择文章专辑")]
    public long AlbumId { get; set; }
}