namespace Dotnet9.Web.ViewModel.BlogPosts;

public record GetBlogPostBriefListResponse(List<BlogPostBrief>? Data, int Total, bool Success, int PageSize,
    int Current);