using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Web.ViewModels.Blogs;

namespace Dotnet9.Web.ViewModels.Homes;

public class HomeViewModel
{
    public List<BlogPostBriefDto> BlogPostsForRecommend { get; set; } = null!;
    public Dictionary<string, LoadMoreKind> LoadMoreKinds { get; set; } = null!;
}