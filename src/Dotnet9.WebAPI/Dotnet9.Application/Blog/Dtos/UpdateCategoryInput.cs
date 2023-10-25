namespace Dotnet9.Application.Blog.Dtos;

public class UpdateCategoryInput : AddCategoryInput
{
    /// <summary>
    /// 分类ID
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}