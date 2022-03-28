namespace Dotnet9.Web.Models;

public class CategoryItem
{
    public CategoryItem(string name, string slug, string cover)
    {
        Name = name;
        Slug = slug;
        Cover = cover;
        Children = new List<CategoryItem>();
    }

    public string Name { get; set; }
    public string Slug { get; set; }
    public string Cover { get; set; }
    public List<CategoryItem>? Children { get; set; }
}