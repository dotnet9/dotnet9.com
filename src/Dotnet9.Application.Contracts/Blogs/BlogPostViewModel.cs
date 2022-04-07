namespace Dotnet9.Application.Contracts.Blogs;

public class BlogPostViewModel
{
    public BlogPostWithDetailsDto BlogPost { get; set; } = null!;
    public BlogPostBriefDto? PreviewBlogPost { get; set; }
    public BlogPostBriefDto? NextBlogPost { get; set; }
    public List<BlogPostBriefDto>? SameCategoryBlogPosts { get; set; }
    public List<BlogPostBriefDto>? SameAlbumBlogPosts { get; set; }
}