namespace Dotnet9.Web.ViewModels.Home;

public class BlogPostListViewModel
{
    public List<BlogPostListItem>? Categories { get; set; }
}

public class BlogPostListItem
{
    public string? CategoryName { get; set; }
    public string? CategorySlug { get; set; }
    public BlogPostBriefForFront? TouTiao { get; set; }
    public List<BlogPostBriefForFront>? BlogPosts { get; set; }
}