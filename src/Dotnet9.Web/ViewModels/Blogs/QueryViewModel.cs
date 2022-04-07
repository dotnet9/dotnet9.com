using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Blogs;

public class QueryViewModel
{
    public string? Query { get; set; }
    public int PageIndex { get; set; }
    public List<BlogPostBriefDto> BlogPosts { get; set; } = null!;
}