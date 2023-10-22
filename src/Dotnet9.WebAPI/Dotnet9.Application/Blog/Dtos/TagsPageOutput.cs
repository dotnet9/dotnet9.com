namespace Dotnet9.Application.Blog.Dtos;

public class TagsPageOutput
{
    /// <summary>
    /// 标签ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 标签名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
    /// <summary>
    /// 标签封面
    /// </summary>
    public string Cover { get; set; }
    /// <summary>
    /// 标签图标
    /// </summary>
    public string Icon { get; set; }
    /// <summary>
    /// 标签颜色 
    /// </summary>
    public string Color { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}