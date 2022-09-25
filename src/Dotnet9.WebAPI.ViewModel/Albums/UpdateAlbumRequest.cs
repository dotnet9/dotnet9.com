namespace Dotnet9.WebAPI.ViewModel.Albums;

public record UpdateAlbumRequest(string[] CategoryNames, int SequenceNumber, string Name, string Slug, string Cover,
    string? Description, bool Visible);