namespace Dotnet9.WebAPI.ViewModels.Links;

public record LinkSeedDto(int SequenceNumber, string Name, string Url,
    string? Description = null, string? Kind = null);