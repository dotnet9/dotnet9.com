namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostListRequest(string? Keywords = null, int Current = 1, int PageSize = 10);