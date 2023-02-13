namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record BlogPostDto(Guid Id, string Title, string Slug, string Description, string Cover,
    string CopyRightType, bool Banner, string? Original, string? OriginalAvatar, string? OriginalTitle,
    string? OriginalLink,
    bool Visible, int ViewCount, int LikeCount, string? AlbumNames, Guid[]? AlbumIds, string? CategoryNames,
    Guid[]? CategoryIds, string? TagNames,
    Guid[]? TagIds, DateTime CreationTime);