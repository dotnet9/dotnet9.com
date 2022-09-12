namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public record GetBlogPostsByTagResponse(IEnumerable<BlogPostDto>? BlogPosts, long TotalCount);