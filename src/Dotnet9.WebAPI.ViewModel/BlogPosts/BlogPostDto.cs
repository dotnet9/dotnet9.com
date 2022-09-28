namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record BlogPostDto(Guid Id, string Title, string Slug, string Description, string Cover,
    string CopyRightType, string? Original, string? OriginalAvatar, string? OriginalTitle, string? OriginalLink,
    bool Visible, string? AlbumNames, Guid[]? AlbumIds, string? CategoryNames, Guid[]? CategoryIds, string? TagNames,
    Guid[]? TagIds);