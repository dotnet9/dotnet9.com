using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.Models;

public class Advertisement : RootEntityTkey<int>
{
    public string? ImageUrl { get; set; }

    public string? Url { get; set; }

    public int CreateUserId { get; set; }

    public virtual UserInfo? CreateUser { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public virtual UserInfo? UpdateUser { get; set; }

    public DateTime UpdateTime { get; set; }
}