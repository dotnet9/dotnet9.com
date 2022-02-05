using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.ViewModels.BlogPosts;

public class BlogPostCommentDto : EntityTKeyDto<int>
{
    public string? Content { get; set; }

    public string? CreateUserName { get; set; }

    public string? CreateUserPortrait { get; set; }

    public DateTime CreateTime { get; set; }

    public string? UpdateUserName { get; set; }

    public string? UpdateUserPortrait { get; set; }

    public DateTime UpdateTime { get; set; }
}