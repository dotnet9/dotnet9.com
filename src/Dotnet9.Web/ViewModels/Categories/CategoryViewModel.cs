using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Web.ViewModels.Categories;

public class CategoryViewModel
{
    public string Name { get; set; } = null!;
    public List<BlogPostWithDetailsDto> Items { get; set; } = null!;
}