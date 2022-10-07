namespace Dotnet9.Domain.Albums;

public class AlbumCount
{
    private AlbumCount()
    {
    }

    public AlbumCount(
        int id,
        string name,
        string slug,
        string cover,
        int blogPostCount)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Cover = cover;
        BlogPostCount = blogPostCount;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public int BlogPostCount { get; set; }
}