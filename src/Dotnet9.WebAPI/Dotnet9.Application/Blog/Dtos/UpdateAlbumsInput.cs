namespace Dotnet9.Application.Blog.Dtos;

public class UpdateAlbumsInput : AddAlbumsInput
{
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}