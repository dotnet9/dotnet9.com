using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.ViewModels.UserInfos;

public class UserInfoDto : RootEntityTkey<int>
{
    public string? UserName { get; set; }

    public string? HeadPortrait { get; set; }

    public int BlogPostCount { get; set; }

    public int QuestionsCount { get; set; }
}