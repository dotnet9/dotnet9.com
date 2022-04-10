using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Shared.Blogs;

namespace Dotnet9.Application.Contracts.Blogs;

public class BlogPostWithDetailsDto
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? BriefDescription { get; set; }

    public string Content { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public CopyrightType CopyrightType { get; set; }

    public string? Original { get; set; }

    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public AlbumBriefDto[]? AlbumNames { get; set; }

    public CategoryBriefDto[]? CategoryNames { get; set; }

    public string[]? TagNames { get; set; }
    public int BrowserCount { get; set; }

    public DateTime? CreateDate { get; set; }
}