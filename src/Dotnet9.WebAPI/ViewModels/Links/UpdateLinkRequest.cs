namespace Dotnet9.WebAPI.ViewModels.Links;

public record UpdateLinkRequest(int SequenceNumber, string Name, string Url, string? Description, LinkKind Kind);