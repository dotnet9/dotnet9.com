namespace Dotnet9.Application.Blog.Dtos;

public class CoversPageOutput
{
    /// <summary>
    /// 模块封面ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 模块名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 模块类型
    /// </summary>
    public CoverType? Type { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
    /// <summary>
    /// 是否显示
    /// </summary>
    public bool IsVisible { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 封面
    /// </summary>
    public string Cover { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}