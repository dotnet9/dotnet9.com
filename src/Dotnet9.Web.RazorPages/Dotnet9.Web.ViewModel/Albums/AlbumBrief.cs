namespace Dotnet9.Web.ViewModel.Albums;

public record AlbumBrief(int SequenceNumber, string Slug, string Name, string? Description, int BlogCount = 0);