namespace Dotnet9.Web.ViewModel.BlogPosts;

public record GetBlogPostBriefListByTagNameResponse(List<BlogPostBriefForFront>? Data,
    int Total,
    bool Success,
    int PageSize,
    int Current);