namespace Dotnet9.WebAPI.ViewModel.Links;

public record LinkSeedDto(int SequenceNumber, string Name, string Url,
    string? Description = null, string? Kind = null);