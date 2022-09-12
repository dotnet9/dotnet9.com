namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public record GetBlogPostsByCategoryResponse(IEnumerable<BlogPostDto>? BlogPosts, long TotalCount);