namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public record GetBlogPostsByCategoryRequest(int Current = 1, int PageSize = 10);