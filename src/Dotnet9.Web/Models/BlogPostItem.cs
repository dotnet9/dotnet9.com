namespace Dotnet9.Web.Models;

public class BlogPostItem
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? BriefDescription { get; set; }

    public bool InBanner { get; set; }

    public string Cover { get; set; } = null!;

    public string[]? Categories { get; set; }

    public string[]? Tags { get; set; }

    public string[]? Albums { get; set; }

    public CopyRightType? CopyrightType { get; set; }

    public string? Original { get; set; }
    public string? OriginalTitle { get; set; }

    public string? OriginalLink { get; set; }

    public string? CreateDate { get; set; }

    public string? UpdateDate { get; set; }

    public string? Content { get; set; }

    public static BlogPostItem ConvertFromMarkdownV2(PostOfMarkdown post)
    {
        return new BlogPostItem
        {
            Title = post.Title!,
            Slug = post.Slug!,
            BriefDescription = post.Description,
            InBanner = post.Banner,
            Cover = post.Cover!,
            Categories = post.Categories,
            Tags = post.Tags,
            Albums = post.Albums,
            CopyrightType = post.Copyright,
            Original = post.Author,
            OriginalTitle = post.OriginalTitle,
            OriginalLink = post.OriginalLink,
            CreateDate = post.Date.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdateDate = post.LastModifyDate?.ToString("yyyy-MM-dd HH:mm:ss"),
            Content = post.Content
        };
    }
}