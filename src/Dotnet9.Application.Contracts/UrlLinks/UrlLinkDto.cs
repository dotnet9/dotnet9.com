namespace Dotnet9.Application.Contracts.UrlLinks;

public class UrlLinkDto
{
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
    public int Index { get; set; }
    public string Kind { get; set; } = null!;
}