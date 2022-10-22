using Dotnet9.Web.ViewModel.Albums;

namespace Dotnet9.Web.ViewModel.BlogPosts;

public record BlogPostDetails(string Title, string Slug, string Description, string Content,
    CopyRightType CopyrightType, string? Original, string? OriginalTitle, string? OriginalLink,
    List<AlbumBrief>? Albums,
    List<CategoryBrief> Categories,
    List<string>? Tags,
    DateTime CreationTime, int ViewCount, BlogPostNear? Preview, BlogPostNear? Next,List<BlogPostNear>? Nears);

public record BlogPostNear(string Title, string Slug, string Cover, string Description, DateTime CreationTime);