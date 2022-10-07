using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Application.Contracts.Albums;

public class AlbumViewModel
{
    public string Name { get; set; } = null!;
    public List<BlogPostBriefDto>? BlogPosts { get; set; }
    public List<AlbumCountDto>? Albums { get; set; }
}