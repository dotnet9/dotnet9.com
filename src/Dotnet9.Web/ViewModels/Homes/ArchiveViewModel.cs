using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Homes;

public class ArchiveViewModel
{
    public List<BlogPostForSitemap> Items { get; set; } = null!;
}