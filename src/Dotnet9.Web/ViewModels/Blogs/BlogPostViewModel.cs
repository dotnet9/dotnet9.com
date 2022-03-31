using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Blogs;

public class BlogPostViewModel
{
    public BlogPostWithDetailsDto BlogPost { get; set; } = null!;
}