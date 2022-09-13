namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public class BlogPostSeedDto
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime? LastModifyDate { get; set; }
    public CopyRightType Copyright { get; set; }
    public string Author { get; set; } = null!;
    public string? OriginalTitle { get; set; }
    public string? OriginalLink { get; set; }
    public bool Draft { get; set; }
    public string Cover { get; set; } = null!;
    public string[]? Albums { get; set; }
    public string[]? Categories { get; set; }
    public string[]? Tags { get; set; }
    public string Content { get; set; } = null!;
}