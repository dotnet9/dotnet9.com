namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostsByAlbumResponse(IEnumerable<BlogPostDto>? BlogPosts, long TotalCount);