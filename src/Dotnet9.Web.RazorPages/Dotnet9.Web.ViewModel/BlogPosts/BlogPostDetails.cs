namespace Dotnet9.Web.ViewModel.BlogPosts;

public record BlogPostDetails(string Title, string Slug, string Description, string Content,
    CopyRightType CopyrightType, string? Original, string? OriginalTitle, string? OriginalLink,
    List<CategoryBrief> Categories,
    DateTime CreationTime, int ViewCount);