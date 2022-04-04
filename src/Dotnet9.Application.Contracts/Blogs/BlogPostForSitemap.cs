namespace Dotnet9.Application.Contracts.Blogs;

public class BlogPostForSitemap
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public DateTime CreateDate { get; set; }
}