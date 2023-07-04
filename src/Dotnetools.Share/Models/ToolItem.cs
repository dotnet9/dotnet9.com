namespace Dotnetools.Share.Models;

public record ToolItem(ToolKind Kind, string Name, string Cover, string Url, string? LearnUrl = null,
    string? Github = null);