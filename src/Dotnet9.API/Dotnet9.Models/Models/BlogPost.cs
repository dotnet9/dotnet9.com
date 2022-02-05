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

    public string? Remark { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<UserInfoBlogPost> UserInfoBlogPosts { get; set; } = new List<UserInfoBlogPost>();


    public virtual ICollection<BlogPostComment> BlogPostComments { get; set; } = new List<BlogPostComment>();

    public int CreateUserId { get; set; }

    public virtual UserInfo? CreateUser { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public virtual UserInfo? UpdateUser { get; set; }

    public DateTime UpdateTime { get; set; }
}