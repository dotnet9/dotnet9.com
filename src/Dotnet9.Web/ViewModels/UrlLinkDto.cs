namespace Dotnet9.Web.ViewModels;

public class UrlLinkDto
{
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public UrlLinkKind Kind { get; set; }
}

public enum UrlLinkKind
{
    Privace,
    Friend
}