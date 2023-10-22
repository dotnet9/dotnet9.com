namespace Dotnet9.Application.Config.Dtos;

public class UpdateCustomConfigItemInput : AddCustomConfigItemInput
{
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}