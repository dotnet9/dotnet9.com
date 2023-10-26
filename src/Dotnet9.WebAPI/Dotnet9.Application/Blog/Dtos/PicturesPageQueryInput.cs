namespace Dotnet9.Application.Blog.Dtos;

public class PicturesPageQueryInput : Pagination
{
    /// <summary>
    /// 模块封面ID
    /// </summary>
    public long Id { get; set; }
}