namespace Dotnet9.Application.Contracts.Blogs;

public class BlogPostBriefDto
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? BriefDescription { get; set; }

    public string Cover { get; set; } = null!;

    public CopyRightType CopyrightType { get; set; }

    public string? Original { get; set; }

    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public DateTimeOffset? CreateDate { get; set; }
}