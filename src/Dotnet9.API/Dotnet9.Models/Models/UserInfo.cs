using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.Models;

public class UserInfo : RootEntityTkey<int>
{
    public string? UserName { get; set; }

    public string? Account { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Introduction { get; set; }

    public string? HeadPortrait { get; set; }

    public DateTime CreateTime { get; set; }
}