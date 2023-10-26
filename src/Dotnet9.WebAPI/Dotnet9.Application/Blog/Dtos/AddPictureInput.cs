namespace Dotnet9.Application.Blog.Dtos;

public class AddPictureInput
{
    /// <summary>
    /// 封面Id
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public long CoverId { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    [Required(ErrorMessage = "请上传图片")]
    public string Url { get; set; }
}