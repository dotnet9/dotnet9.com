namespace Dotnet9.WebAPI.ViewModels.Links;

public record LinkDto(Guid Id, int SequenceNumber, string Name, string Url,
    string? Description = null, LinkKind Kind = LinkKind.Friend);