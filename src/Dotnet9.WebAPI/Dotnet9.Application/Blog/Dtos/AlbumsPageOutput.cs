namespace Dotnet9.Application.Blog.Dtos;

public class AlbumsPageOutput
{
    /// <summary>
    /// 专辑ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 专辑名称
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 专辑别名
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 标签封面
    /// </summary>
    public string Cover { get; set; } = null!;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}