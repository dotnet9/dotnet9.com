namespace Dotnet9.Application.Contracts.Albums;

public class AlbumCountDto : EntityDto
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public int BlogPostCount { get; set; }
}