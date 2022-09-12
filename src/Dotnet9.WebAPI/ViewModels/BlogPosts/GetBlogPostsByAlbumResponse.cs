namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public record GetBlogPostsByAlbumResponse(IEnumerable<BlogPostDto>? BlogPosts, long TotalCount);