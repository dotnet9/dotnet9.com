namespace Dotnet9.Domain.Shared.Blogs;

public class BlogSeedDto
{
    public string? Title { get; set; }

    public string? Slug { get; set; }
    public string? BriefDescription { get; set; }
    public string? Cover { get; set; }
    public bool InBanner { get; set; }
    public string[]? Albums { get; set; }
    public string[]? Categories { get; set; }
    public string[]? Tags { get; set; }
    public string? CopyrightType { get; set; }
    public string? Original { get; set; }
    public string? OriginalLink { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public string? Content { get; set; }
}