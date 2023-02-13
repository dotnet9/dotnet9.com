namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostListResponse(IEnumerable<BlogPostDto>? Records, long Count);