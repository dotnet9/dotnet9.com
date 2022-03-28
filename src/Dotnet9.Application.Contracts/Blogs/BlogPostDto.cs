using Dotnet9.Domain.Shared.Blogs;

namespace Dotnet9.Application.Contracts.Blogs;

public class BlogPostDto : EntityDto
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string BriefDescription { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string Content { get; set; } = null!;

    public CopyrightType CopyrightType { get; set; }

    public string? Original { get; set; }
    public string? OriginalAvatar { get; set; }

    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public string[]? AlbumNames { get; set; }

    public string[]? CategoryNames { get; set; }

    public string[]? TagNames { get; set; }

    public DateTime CreateDate { get; set; }
}