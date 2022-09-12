namespace Dotnet9.WebAPI.ViewModels.BlogPosts;

public record GetBlogPostListRequest(string? Keywords, int PageIndex, int PageSize);