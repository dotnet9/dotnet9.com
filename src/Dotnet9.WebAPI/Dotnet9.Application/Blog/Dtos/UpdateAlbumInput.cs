namespace Dotnet9.Application.Blog.Dtos;

public class UpdateAlbumInput : AddAlbumInput
{
    /// <summary>
    /// 标签ID
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}