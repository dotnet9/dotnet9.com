namespace Dotnet9.Web.ViewModels.Homes;

public class HomeViewModel
{
    public List<BlogPostBriefDto> BlogPostsForRecommend { get; set; } = null!;
    public Dictionary<string, LoadMoreKind> LoadMoreKinds { get; set; } = null!;
}