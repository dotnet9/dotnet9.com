namespace Dotnet9.Application.Blog.Dtos;

public class CoversPageQueryInput : Pagination
{
    /// <summary>
    /// 模块名称
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 模块类型
    /// </summary>
    public CoverType? Type { get; set; }
}