namespace Dotnet9.WebAPI.ViewModel.Categories;

public class CategoryTreeItem
{
    public string Title { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Key { get; set; } = null!;

    public List<CategoryTreeItem> Children { get; } = new();
}