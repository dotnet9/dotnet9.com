namespace Dotnet9.Application.Blog.Dtos;

public class UpdateCoversInput : AddCoversInput
{
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}