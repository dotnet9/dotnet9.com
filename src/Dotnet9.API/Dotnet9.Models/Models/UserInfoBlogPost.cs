using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.Models;

public class UserInfoBlogPost : RootEntityTkey<int>
{
    public int UserId { get; set; }

    public int BlogPostId { get; set; }
}