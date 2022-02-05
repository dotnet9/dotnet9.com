namespace Dotnet9.Models.ViewModels.UserInfos;

public class CreateUserInfoInputDto
{
    public string? UserName { get; set; }

    public string? Account { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Introduction { get; set; }

    public string? HeadPortrait { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}