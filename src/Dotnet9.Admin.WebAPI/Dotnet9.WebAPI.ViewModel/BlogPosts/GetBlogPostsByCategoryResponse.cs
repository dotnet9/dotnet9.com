namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostsByCategoryResponse(IEnumerable<BlogPostDto>? Records, long TotalCount);