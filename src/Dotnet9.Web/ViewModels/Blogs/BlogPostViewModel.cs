using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Blogs;

public class BlogPostViewModel
{
    public BlogPostWithDetailsDto BlogPost { get; set; } = null!;
    public BlogPostWithDetailsDto? PreviewBlogPost { get; set; }
    public BlogPostWithDetailsDto? NextBlogPost { get; set; }
    public List<BlogPostWithDetailsDto>? SameCategoryBlogPosts { get; set; }
    public List<BlogPostWithDetailsDto>? SameAlbumBlogPosts { get; set; }
}