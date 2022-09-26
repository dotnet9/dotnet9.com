namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostListResponse(IEnumerable<BlogPostDto>? Data, long Total, bool Success, int PageSize,
    int Current);