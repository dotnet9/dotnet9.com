namespace Dotnet9.Web.ViewModels.Blogs;

public class HomeHeaderViewModel
{
    public List<BlogPostBriefDto>? RecommendItems { get; set; }
    public BlogPostBriefDto? First { get; set; }
    public List<BlogPostBriefDto>? LatestItems { get; set; }
}