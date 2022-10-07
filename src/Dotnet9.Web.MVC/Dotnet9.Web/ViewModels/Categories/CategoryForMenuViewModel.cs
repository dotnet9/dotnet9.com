namespace Dotnet9.Web.ViewModels.Categories;

public class CategoryForMenuViewModel
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public List<CategoryForMenuViewModel>? Children { get; set; }
}