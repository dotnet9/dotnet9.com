namespace Dotnet9.WebAPI.ViewModels;

public static class BlogPostExtension
{
    public static BlogPostDetailDto? ConvertToBlogPostDetailDto(this BlogPost? blogPost, Dotnet9DbContext dbContext)
    {
        if (blogPost == null)
        {
            return null;
        }

        string[]? albumNames = null;
        if (blogPost.Albums != null && blogPost.Albums.Any())
        {
            Guid[] albumIds = blogPost.Albums?.Select(album => album.AlbumId).ToArray()!;
            albumNames = dbContext.Albums!.Where(album => albumIds.Contains(album.Id)).Select(album => album.Name)
                .ToArray();
        }

        string[]? categoryNames = null;
        if (blogPost.Categories != null && blogPost.Categories.Any())
        {
            Guid[] categoryIds = blogPost.Categories?.Select(category => category.CategoryId).ToArray()!;
            categoryNames = dbContext.Categories!.Where(category => categoryIds.Contains(category.Id))
                .Select(category => category.Name).ToArray();
        }

        string[]? tagNames = null;
        if (blogPost.Tags != null && blogPost.Tags.Any())
        {
            Guid[] tagIds = blogPost.Tags?.Select(tag => tag.TagId).ToArray()!;
            tagNames = dbContext.Tags!.Where(tag => tagIds.Contains(tag.Id)).Select(tag => tag.Name!).ToArray();
        }

        return new BlogPostDetailDto(blogPost.Id, blogPost.Title, blogPost.Slug, blogPost.Description, blogPost.Cover,
            blogPost.Content,
            blogPost.CopyrightType.GetDescription(), blogPost.Original, blogPost.OriginalAvatar,
            blogPost.OriginalTitle, blogPost.OriginalLink, blogPost.Visible, albumNames, categoryNames, tagNames);
    }

    public static BlogPostDto? ConvertToBlogPostDto(this BlogPost? blogPost, Dotnet9DbContext dbContext)
    {
        if (blogPost == null)
        {
            return null;
        }

        string? albumNames = null;
        Guid[]? albumIds = null;
        if (blogPost.Albums != null && blogPost.Albums.Any())
        {
            Guid[] allAlbumIds = blogPost.Albums?.Select(album => album.AlbumId).ToArray()!;
            IQueryable<Album> queryList = dbContext.Albums!.Where(album => allAlbumIds.Contains(album.Id));
            albumNames = queryList.Select(album => album.Name).JoinAsString(",");
            albumIds = queryList.Select(x => x.Id).ToArray();
        }

        string? categoryNames = null;
        Guid[]? categoryIds = null;
        if (blogPost.Categories != null && blogPost.Categories.Any())
        {
            Guid[] allCategoryIds = blogPost.Categories?.Select(category => category.CategoryId).ToArray()!;
            IQueryable<Category> queryList =
                dbContext.Categories!.Where(category => allCategoryIds.Contains(category.Id));
            categoryNames = queryList.Select(category => category.Name).JoinAsString(",");
            categoryIds = queryList.Select(x => x.Id).ToArray();
        }

        string? tagNames = null;
        Guid[]? tagIds = null;
        if (blogPost.Tags != null && blogPost.Tags.Any())
        {
            Guid[] allTagIds = blogPost.Tags?.Select(tag => tag.TagId).ToArray()!;
            IQueryable<Tag> queryList = dbContext.Tags!.Where(tag => allTagIds.Contains(tag.Id));
            tagNames = queryList.Select(tag => tag.Name!).JoinAsString(",");
            tagIds = queryList.Select(x => x.Id).ToArray();
        }

        return new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.Content, blogPost.Slug, blogPost.Description,
            blogPost.Cover,
            blogPost.CopyrightType.GetDescription(), blogPost.Banner, blogPost.Original, blogPost.OriginalAvatar,
            blogPost.OriginalTitle, blogPost.OriginalLink, blogPost.Visible, blogPost.ViewCount, blogPost.LikeCount,
            albumNames, albumIds,
            categoryNames, categoryIds, tagNames, tagIds, blogPost.CreationTime, blogPost.IsDeleted);
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