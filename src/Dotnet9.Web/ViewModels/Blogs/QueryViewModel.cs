namespace Dotnet9.Web.ViewModels.Blogs;

public class QueryViewModel
{
    public string? Query { get; set; }
    public int PageIndex { get; set; }
    public int PageCount { get; set; }
    public int Total { get; set; }
    public List<BlogPostBriefDto>? BlogPosts { get; set; }
}