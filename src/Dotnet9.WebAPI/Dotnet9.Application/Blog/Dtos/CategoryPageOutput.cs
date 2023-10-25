namespace Dotnet9.Application.Blog.Dtos;

public class CategoryPageOutput
{
    /// <summary>
    /// 分类ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 父级id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    public string Cover { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 子分类
    /// </summary>
    public List<CategoryPageOutput> Children { get; set; } = new();
}