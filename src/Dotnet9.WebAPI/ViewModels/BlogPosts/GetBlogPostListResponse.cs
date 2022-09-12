namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public record GetBlogPostListResponse(IEnumerable<BlogPostDto>? BlogPosts, long TotalCount);