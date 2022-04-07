using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Application.Contracts.Tags;

public class TagViewModel
{
    public List<TagCountDto>? Tags { get; set; }
    public string? TagName { get; set; }
    public List<BlogPostBriefDto>? BlogPosts { get; set; }
}