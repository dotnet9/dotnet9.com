

namespace Dotnet9.Contracts.Dto.Blogs;

public record BlogBrief(Guid Id, string Title, string Slug, string Description, string Cover,
    string CopyrightType, string? Original, string? OriginalTitle, string? OriginalLink, bool Banner,
    List<CategoryBrief>? Categories, List<AlbumBrief>? Albums, List<TagBrief>? Tags, int ViewCount,
    DateTime CreationTime);