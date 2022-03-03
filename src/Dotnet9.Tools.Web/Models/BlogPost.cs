namespace Dotnet9.Tools.Web.Models;

public class BlogPost
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

    public string? UpdateDate { get; set; }

    public string? Content { get; set; }
}

public static class BlogPostExtensions
{
    public static bool IsExist(this BlogPost post, string filter)
    {
        if (!string.IsNullOrWhiteSpace(post.Title) && post.Title.ToLower().Contains(filter.ToLower())) return true;

        if (!string.IsNullOrWhiteSpace(post.Slug) && post.Slug.ToLower().Contains(filter.ToLower())) return true;

        if (!string.IsNullOrWhiteSpace(post.Description) &&
            post.Description.ToLower().Contains(filter.ToLower())) return true;

        if (post.Categories != null && post.Categories.Contains(filter)) return true;

        if (post.Tags != null && post.Tags.Contains(filter)) return true;

        if (post.Albums != null && post.Albums.Contains(filter)) return true;

        if (!string.IsNullOrWhiteSpace(post.Original) && post.Original.ToLower().Contains(filter.ToLower()))
            return true;

        if (!string.IsNullOrWhiteSpace(post.OriginalLink) &&
            post.OriginalLink.ToLower().Contains(filter.ToLower())) return true;

        return !string.IsNullOrWhiteSpace(post.Content) && post.Content.ToLower().Contains(filter.ToLower());
    }
}