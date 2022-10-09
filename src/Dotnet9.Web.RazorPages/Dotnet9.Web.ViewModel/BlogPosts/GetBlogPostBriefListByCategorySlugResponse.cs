namespace Dotnet9.Web.ViewModel.BlogPosts;

public record GetBlogPostBriefListByCategorySlugResponse(string? CategoryName, List<BlogPostBrief>? Data, int Total,
    bool Success,
    int PageSize,
    int Current);