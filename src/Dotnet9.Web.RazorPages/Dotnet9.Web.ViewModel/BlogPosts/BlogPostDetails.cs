namespace Dotnet9.Web.ViewModel.BlogPosts;

public record BlogPostDetails(string Title, string Slug, string Description, string Content, string? Original,
    List<CategoryBrief> Categories,
    DateTime CreationTime, int ViewCount);