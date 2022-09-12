namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public record BlogPostDto(Guid Id, string Title, string Slug, string Description, string Cover, string Content,
    CopyRightType CopyRightType, string? Original, string? OriginalAvatar, string? OriginalTitle, string? OriginalLink,
    bool Visible, Guid[]? AlbumIds, Guid[]? CategoryIds, Guid[]? TagIds);

public static class BlogPostExtension
{
    public static BlogPostDto? ConvertToBlogPostDto(this BlogPost? blogPost)
    {
        if (blogPost == null)
        {
            return null;
        }

        var albumIds = blogPost.Albums?.Select(album => album.AlbumId).ToArray();
        var categoryIds = blogPost.Categories?.Select(category => category.CategoryId).ToArray();
        var tagIds = blogPost.Tags?.Select(tag => tag.TagId).ToArray();
        return new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.Slug, blogPost.Description, blogPost.Cover,
            blogPost.Content, blogPost.CopyrightType, blogPost.Original, blogPost.OriginalAvatar,
            blogPost.OriginalTitle, blogPost.OriginalLink, blogPost.Visible, albumIds, categoryIds, tagIds);
    }

    public static BlogPostDto[]? ConvertToBlogPostDtoArray(this BlogPost[]? blogPosts)
    {
        if (blogPosts == null || !blogPosts.Any())
        {
            return null;
        }

        return blogPosts.Select(blogPost => blogPost.ConvertToBlogPostDto()!).ToArray();
    }
}