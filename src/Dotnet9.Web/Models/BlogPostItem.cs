using Dotnet9.Domain.Shared.Blogs;

namespace Dotnet9.Web.Models;

public class BlogPostItem
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? BriefDescription { get; set; }

    public bool InBanner { get; set; }

    public string Cover { get; set; } = null!;

    public string[]? Categories { get; set; }

    public string[]? Tags { get; set; }

    public string[]? Albums { get; set; }

    public CopyrightType? CopyrightType { get; set; }

    public string? Original { get; set; }
    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public string? CreateDate { get; set; }

    public string? UpdateDate { get; set; }

    public string? Content { get; set; }
}