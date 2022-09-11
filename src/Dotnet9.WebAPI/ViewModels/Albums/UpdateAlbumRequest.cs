namespace Dotnet9.WebAPI.ViewModels.Albums;

public class UpdateAlbumRequest
{
    public Guid[] CategoryIds { get; set; } = null!;
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public string? Description { get; set; }
    public bool Visible { get; set; }
}