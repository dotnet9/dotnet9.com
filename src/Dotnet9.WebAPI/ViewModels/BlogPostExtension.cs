namespace Dotnet9.WebAPI.ViewModels;

public static class BlogPostExtension
{
    public static BlogPostDto? ConvertToBlogPostDto(this BlogPost? blogPost, Dotnet9DbContext dbContext)
    {
        if (blogPost == null)
        {
            return null;
        }

        string[]? albumNames = null;
        if (blogPost.Albums != null && blogPost.Albums.Any())
        {
            var albumIds = blogPost.Albums?.Select(album => album.AlbumId).ToArray()!;
            albumNames = dbContext.Albums!.Where(album => albumIds.Contains(album.Id)).Select(album => album.Name)
                .ToArray();
        }

        string[]? categoryNames = null;
        if (blogPost.Categories != null && blogPost.Categories.Any())
        {
            var categoryIds = blogPost.Categories?.Select(category => category.CategoryId).ToArray()!;
            categoryNames = dbContext.Categories!.Where(category => categoryIds.Contains(category.Id))
                .Select(category => category.Name).ToArray();
        }

        string[]? tagNames = null;
        if (blogPost.Tags != null && blogPost.Tags.Any())
        {
            var tagIds = blogPost.Tags?.Select(tag => tag.TagId).ToArray()!;
            tagNames = dbContext.Tags!.Where(tag => tagIds.Contains(tag.Id)).Select(tag => tag.Name!).ToArray();
        }

        return new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.Slug, blogPost.Description, blogPost.Cover,
            blogPost.CopyrightType.GetDescription(), blogPost.Original, blogPost.OriginalAvatar,
            blogPost.OriginalTitle, blogPost.OriginalLink, blogPost.Visible, albumNames, categoryNames, tagNames);
    }

    public static BlogPostDto[]? ConvertToBlogPostDtoArray(this BlogPost[]? blogPosts, Dotnet9DbContext dbContext)
    {
        if (blogPosts == null || !blogPosts.Any())
        {
            return null;
        }

        return blogPosts.Select(blogPost => blogPost.ConvertToBlogPostDto(dbContext)!).ToArray();
    }
}