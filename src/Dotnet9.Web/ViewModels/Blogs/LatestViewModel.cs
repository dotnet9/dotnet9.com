using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Blogs;

public class LatestViewModel
{
    public List<BlogPostWithDetailsDto> BlogPosts { get; set; } = null!;
}