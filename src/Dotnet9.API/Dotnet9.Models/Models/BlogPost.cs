namespace Dotnet9.Models.Models;

public class BlogPost
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public CopyrightType CopyrightType { get; set; }

    public string? CoverImageUrl { get; set; }

    public string? Original { get; set; }

    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public string? Categories { get; set; }

    public string? Albums { get; set; }

    public string? Tags { get; set; }

    public string? Content { get; set; }

    public int Traffic { get; set; }

    public int CommentNum { get; set; }

    public string? Creator { get; set; }

    public DateTime CreateTime { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime UpdateTime { get; set; }

    public string? Remark { get; set; }

    public bool? IsDeleted { get; set; }
}

public enum CopyrightType
{
    Default,
    Contribution,
    Reprint
}