namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public record AddBlogPostRequest(string Title, string Slug, string Description, string Cover, string Content,
    CopyRightType CopyRightType, string? Original, string? OriginalAvatar, string? OriginalTitle, string? OriginalLink,
    bool Visible, Guid[]? AlbumIds, Guid[]? CategoryIds, Guid[]? TagIds);