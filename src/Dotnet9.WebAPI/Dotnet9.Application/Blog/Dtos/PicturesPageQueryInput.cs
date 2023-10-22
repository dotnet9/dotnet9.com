namespace Dotnet9.Application.Blog.Dtos;

public class PicturesPageQueryInput : Pagination
{
    /// <summary>
    /// 相册ID
    /// </summary>
    public long Id { get; set; }
}