namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostsByTagResponse(IEnumerable<BlogPostDto>? BlogPosts, long TotalCount);