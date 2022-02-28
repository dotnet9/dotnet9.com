namespace Dotnet9.Tools.Web.Models;

public class PostInfo
{
    public string? Title { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string[]? Categories { get; set; }

    public string[]? Tags { get; set; }

    public string[]? Albums { get; set; }

    public string? CopyrightType { get; set; }

    public string? Original { get; set; }

    public string? OriginalLink { get; set; }

    public string? CreateDate { get; set; }

    public string? Content { get; set; }
}