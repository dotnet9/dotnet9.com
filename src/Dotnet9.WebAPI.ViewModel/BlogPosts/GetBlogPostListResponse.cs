namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostListResponse(IEnumerable<BlogPostDto>? BlogPosts, long TotalCount);