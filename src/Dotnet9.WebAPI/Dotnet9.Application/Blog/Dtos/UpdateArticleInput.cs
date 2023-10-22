namespace Dotnet9.Application.Blog.Dtos;

public class UpdateArticleInput : AddArticleInput
{
    /// <summary>
    /// 文章ID
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}