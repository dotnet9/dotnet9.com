using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Albums;

public class AlbumViewModel
{
    public string Name { get; set; } = null!;
    public List<BlogPostWithDetailsDto> Items { get; set; } = null!;
}