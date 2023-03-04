namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetTopAndFeaturedBlogPostResponse(BlogPostDetailDto Top, BlogPostDetailDto[] Featured);