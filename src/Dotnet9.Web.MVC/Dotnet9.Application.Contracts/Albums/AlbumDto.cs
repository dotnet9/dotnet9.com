namespace Dotnet9.Application.Contracts.Albums;

public class AlbumDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Slug { get; set; } = null!;
    public string? Name { get; set; }
    public string? Cover { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public bool IsShow { get; set; }
}