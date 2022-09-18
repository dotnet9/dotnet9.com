namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record UpdateBlogPostRequest(Guid Id, string Title, string Slug, string Description, string Cover,
    string Content,
    CopyRightType CopyRightType, string? Original, string? OriginalAvatar, string? OriginalTitle, string? OriginalLink,
    bool Visible, Guid[]? AlbumIds, Guid[]? CategoryIds, Guid[]? TagIds);