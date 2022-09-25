namespace Dotnet9.WebAPI.ViewModel.Albums;

public record AddAlbumRequest(string[] CategoryNames, int SequenceNumber, string Name, string Slug, string Cover,
    string? Description, bool Visible);