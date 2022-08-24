namespace Dotnet9.Core.FileHelpers;

public class PostOfMarkdownV1
{
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Cover { get; set; }
    public string? BriefDescription { get; set; }
    public string[]? Categories { get; set; }
    public string[]? Albums { get; set; }
    public string[]? Tags { get; set; }
    public string? CopyrightType { get; set; }
    public string? Original { get; set; }
    public string? OriginalLink { get; set; }
    public string? CreateDate { get; set; }
    public bool Banner { get; set; }
    public string? Content { get; set; }
}