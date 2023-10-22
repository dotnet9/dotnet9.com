namespace Dotnet9.Application.Blog.Dtos;

public class AlbumsPageQueryInput : Pagination
{
    /// <summary>
    /// 相册名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 相册类型
    /// </summary>
    public CoverType? Type { get; set; }
}