namespace Dotnet9.Service.Application.FriendlyLinks.Commands;

public record FriendlyLinkCommand(int Index, string Name, string Url, string? Description) : Command
{
}