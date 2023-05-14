namespace Dotnet9.Contracts.Dto.FriendlyLinks;

public record FriendlyLinkDto
{
    public Guid Id { get; set; }

    public int Index { get; set; }

    public string Name { get; set; } = default!;

    public string Url { get; set; } = default!;

    public string? Description { get; set; }
}