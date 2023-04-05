namespace Dotnet9.Contracts.IntegrationEvents;

public record FriendlyLinkCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }

    public int Index { get; set; }

    public string Name { get; set; } = default!;

    public string Url { get; set; } = default!;

    public string? Description { get; set; }
}