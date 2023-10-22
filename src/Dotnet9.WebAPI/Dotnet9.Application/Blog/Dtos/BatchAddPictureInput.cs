namespace Dotnet9.Application.Blog.Dtos;

public class BatchAddPictureInput
{
    [Required(ErrorMessage = "请上传图片")]
    public List<AddPictureInput> Pictures { get; set; }
}