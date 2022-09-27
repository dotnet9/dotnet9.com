namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record BlogPostDetailDto(Guid Id, string Title, string Slug, string Description, string Cover, string Content,
    string CopyRightType, string? Original, string? OriginalAvatar, string? OriginalTitle, string? OriginalLink,
    bool Visible, string[]? AlbumNames, string[]? CategoryNames, string[]? TagNames);