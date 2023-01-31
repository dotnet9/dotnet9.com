namespace Dotnet9.WebAPI.ViewModel.Albums;

public record AlbumDto
{
    public Guid Id { get; set; }
    public string? CategoryNames { get; set; }
    public Guid[]? CategoryIds { get; set; }
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Cover { get; set; } = null!;
    public int BlogPostCount { get; set; }
    public DateTime CreationName { get; set; }

    public string? Description { get; set; }
    public bool Visible { get; set; }
}