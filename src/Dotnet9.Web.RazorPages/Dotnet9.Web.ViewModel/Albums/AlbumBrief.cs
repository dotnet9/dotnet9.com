namespace Dotnet9.Web.ViewModel.Albums;

public record AlbumBrief(string Slug, string Name, string? Description, int BlogCount = 0);