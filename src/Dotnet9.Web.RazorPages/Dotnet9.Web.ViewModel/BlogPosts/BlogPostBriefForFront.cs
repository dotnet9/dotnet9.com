namespace Dotnet9.Web.ViewModel.BlogPosts;

public record BlogPostBriefForFront(string Title, string Slug, string Description, string? Original,
    List<CategoryBrief> Categories,
    DateTime CreationTime, int ViewCount);