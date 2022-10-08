namespace Dotnet9.Web.ViewModel.BlogPosts;

public record GetBlogPostBriefListByCategorySlugRequest(string Slug, int Current, int PageSize);