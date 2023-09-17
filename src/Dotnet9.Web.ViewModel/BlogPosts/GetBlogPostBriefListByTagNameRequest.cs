namespace Dotnet9.Web.ViewModel.BlogPosts;

public record GetBlogPostBriefListByTagNameRequest(string Name, int Current, int PageSize);