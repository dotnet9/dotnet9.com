namespace Dotnet9.Web.Models;

public class AlbumItem
{
    public AlbumItem(string name, string slug, string cover)
    {
        Name = name;
        Cover = cover;
        Slug = slug;
        Children = new List<AlbumItem>();
    }

    public string Name { get; set; }
    public string Slug { get; set; }
    public string Cover { get; set; }
    public List<AlbumItem>? Children { get; set; }
}