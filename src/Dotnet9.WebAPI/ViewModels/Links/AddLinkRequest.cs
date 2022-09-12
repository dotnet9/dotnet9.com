namespace Dotnet9.WebAPI.ViewModels.Links;

public record AddLinkRequest(int SequenceNumber, string Name, string Url, string? Description, LinkKind Kind);