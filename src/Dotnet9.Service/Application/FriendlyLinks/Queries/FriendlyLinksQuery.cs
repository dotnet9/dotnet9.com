namespace Dotnet9.Service.Application.FriendlyLinks.Queries;

public record FriendlyLinksQuery : ItemsQueryBase<PaginatedListBase<FriendlyLinkDto>>
{
    public string? Name { get; set; }

    public override PaginatedListBase<FriendlyLinkDto> Result { get; set; } = default!;
}