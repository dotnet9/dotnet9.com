namespace Dotnet9.Application.Contracts.Albums;

public class AlbumDto : EntityDto
{
    public string? Name { get; set; }

    public string? Cover { get; set; }

    public string? Description { get; set; }
}