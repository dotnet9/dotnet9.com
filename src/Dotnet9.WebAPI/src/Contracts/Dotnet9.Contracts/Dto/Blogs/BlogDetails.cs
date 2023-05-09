namespace Dotnet9.Contracts.Dto.Blogs;

public record BlogDetails(Guid Id, string Title, string Slug, string Description, string Cover, string Content,
    string CopyrightType, string? Original, string? OriginalTitle, string? OriginalLink, bool Banner,
    List<CategoryBrief>? Categories, List<AlbumBrief>? Albums, List<TagBrief>? Tags, int ViewCount, int LikeCount,
    BlogPostNear? Preview, BlogPostNear? Next,
    List<BlogPostNear>? Nears,
    DateTime CreationTime);

public record BlogPostNear(string Title, string Slug, string Cover, string Description, DateTime CreationTime);