using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.Models;

public class BlogPostComment : RootEntityTkey<int>
{
    public int BlogPostId { get; set; }

    public virtual BlogPost? BlogPost { get; set; }

    public string? Content { get; set; }

    public int CreateUserId { get; set; }

    public virtual UserInfo? CreateUser { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public virtual UserInfo? UpdateUser { get; set; }

    public DateTime UpdateTime { get; set; }
}