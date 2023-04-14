namespace Dotnet9.Contracts.Dto.Albums;

public record AlbumBrief(string Name, string Slug, string Cover, string? Description, int BlogCount);