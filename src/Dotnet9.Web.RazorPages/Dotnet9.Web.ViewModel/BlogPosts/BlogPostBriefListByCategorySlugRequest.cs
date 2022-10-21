namespace Dotnet9.Web.ViewModel.BlogPosts;

public record BlogPostBriefListByCategorySlugRequest(string Slug, int Current, int PageSize);