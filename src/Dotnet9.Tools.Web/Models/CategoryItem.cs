namespace Dotnet9.Tools.Web.Models;

public class CategoryItem
{
    public CategoryItem(string name, string slug)
    {
        Name = name;
        Slug = slug;
        Children = new List<CategoryItem>();
    }

    public string Name { get; set; }
    public string Slug { get; set; }
    public List<CategoryItem>? Children { get; set; }
}