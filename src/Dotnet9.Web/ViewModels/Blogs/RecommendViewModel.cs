using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Blogs;

public class RecommendViewModel
{
    public List<BlogPostBriefDto>? BlogPostsForRecommend { get; set; }
}