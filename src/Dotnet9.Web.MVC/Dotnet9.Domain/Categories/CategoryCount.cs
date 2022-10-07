namespace Dotnet9.Domain.Categories;

public class CategoryCount
{
    private CategoryCount()
    {
    }

    public CategoryCount(
        int id,
        int? parentId,
        string name,
        string slug,
        string cover,
        int blogPostCount)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
        Slug = slug;
        Cover = cover;
        BlogPostCount = blogPostCount;
    }

    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public int BlogPostCount { get; set; }
}