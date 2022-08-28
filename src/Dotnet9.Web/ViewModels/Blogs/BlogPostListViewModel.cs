namespace Dotnet9.Web.ViewModels.Blogs;

public class BlogPostListViewModel
{
    public string Name { get; set; } = null!;
    public List<BlogPostBriefDto>? Items { get; set; }
}