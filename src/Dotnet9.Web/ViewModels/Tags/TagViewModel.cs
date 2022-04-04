using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Tags;

namespace Dotnet9.Web.ViewModels.Tags;

public class TagViewModel
{
    public List<TagCountDto>? Tags { get; set; }
    public string? TagName { get; set; }
    public List<BlogPostWithDetailsDto>? BlogPosts { get; set; }
}