using Dotnet9.Domain.Shared.Blogs;

namespace Dotnet9.Domain.Blogs;

public class BlogPostBrief
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? BriefDescription { get; set; }

    public string Cover { get; set; } = null!;

    public CopyrightType CopyrightType { get; set; }

    public string? Original { get; set; }

    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public DateTimeOffset? CreateDate { get; set; }
}