namespace Dotnet9.Application.Blog.Dtos;

public class UpdateTalksInput : AddTalksInput
{
    /// <summary>
    /// 说说主键
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}