namespace Dotnet9.Service.Application.FriendlyLinks.Commands;

public record FriendlyLinkCommand : Command
{
    public int Index { get; set; }

    public string Name { get; set; } = default!;

    public string Url { get; set; } = default!;

    public string? Description { get; set; }
}