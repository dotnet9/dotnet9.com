namespace Dotnet9.Web.ViewModel.BlogPosts;

public record GetBlogPostBriefListByAlbumSlugResponse(string? AlbumName, List<BlogPostBriefForFront>? Data,
    int Total,
    bool Success,
    int PageSize,
    int Current);