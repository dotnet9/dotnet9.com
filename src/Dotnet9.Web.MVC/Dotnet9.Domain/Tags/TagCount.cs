namespace Dotnet9.Domain.Tags;

public class TagCount
{
    private TagCount()
    {
    }

    public TagCount(
        int id,
        string name,
        int blogPostCount)
    {
        Id = id;
        Name = name;
        BlogPostCount = blogPostCount;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BlogPostCount { get; set; }
}