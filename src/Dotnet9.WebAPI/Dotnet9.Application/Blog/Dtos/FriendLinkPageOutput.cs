namespace Dotnet9.Application.Blog.Dtos;

public class FriendLinkPageOutput
{
    /// <summary>
    /// 友情链接主键
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
    /// <summary>
    /// 站点名称
    /// </summary>
    public string SiteName { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
    /// <summary>
    /// 忽略站点检查
    /// </summary>
    public bool IsIgnoreCheck { get; set; }
    /// <summary>
    /// 友链
    /// </summary>
    public string Link { get; set; }
    /// <summary>
    /// Logo链接
    /// </summary>
    public string Logo { get; set; }
    /// <summary>
    /// 对方博客友情链接地址
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Remark { get; set; }
}