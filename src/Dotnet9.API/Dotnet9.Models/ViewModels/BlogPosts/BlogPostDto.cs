namespace Dotnet9.Models.ViewModels.BlogPosts;

public class BlogPostDto
{
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

    public string? Remark { get; set; }

    public int CreateUserId { get; set; }

    public string? CreateUserName { get; set; }

    public string? CreateUserPortrait { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public string? UpdateUserName { get; set; }

    public string? UpdateUserPortrait { get; set; }

    public DateTime UpdateTime { get; set; }
}