namespace Dotnet9.Application.Blog.Dtos;

public class UpdateTagInput : AddTagInput
{
    /// <summary>
    /// 标签ID
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}