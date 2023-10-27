namespace Dotnet9.Application.Blog.Dtos;

public class AlbumsPageQueryInput : Pagination
{
    /// <summary>
    /// 专辑名称 
    /// </summary>
    public string Name { get; set; }
}