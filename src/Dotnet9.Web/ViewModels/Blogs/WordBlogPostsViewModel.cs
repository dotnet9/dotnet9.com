using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Blogs;

public class WordBlogPostsViewModel
{
    public Dictionary<string, CategoryWordBlogPosts>? Items { get; set; }
}

public class CategoryWordBlogPosts
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public List<BlogPostBriefDto>? BlogPosts { get; set; }
}