namespace Dotnet9.WebAPI.ViewModel.Categories;

public class CategoryTreeItemDto
{
    public string Title { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Key { get; set; } = null!;

    public List<CategoryTreeItemDto> Children { get; } = new();
}