namespace Dotnet9.Commons.FileHelpers;

public class PostOfMarkdown
{
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime? LastModifyDate { get; set; }
    public bool Banner { get; set; }
    public CopyRightType Copyright { get; set; }
    public string? Author { get; set; }
    public string? OriginalTitle { get; set; }
    public string? OriginalLink { get; set; }
    public bool Draft { get; set; }
    public string? Cover { get; set; }
    public string[]? Albums { get; set; }
    public string[]? Categories { get; set; }
    public string[]? Tags { get; set; }
    public string? Content { get; set; }
}

public enum CopyRightType
{
    [EnumMember(Value = "default")] Default,
    [EnumMember(Value = "contribution")] Contribution,
    [EnumMember(Value = "reprint")] Reprint
}