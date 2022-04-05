using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Homes;

public class HomeViewModel
{
    public List<BlogPostWithDetailsDto> BlogPostsForRecommend { get; set; } = null!;
}