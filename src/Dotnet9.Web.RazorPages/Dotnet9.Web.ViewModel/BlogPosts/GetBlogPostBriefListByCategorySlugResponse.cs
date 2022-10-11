namespace Dotnet9.Web.ViewModel.BlogPosts;

public record GetBlogPostBriefListByCategorySlugResponse(string? CategoryName, List<BlogPostBriefForFront>? Data, int Total,
    bool Success,
    int PageSize,
    int Current);