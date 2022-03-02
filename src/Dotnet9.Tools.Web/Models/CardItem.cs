namespace Dotnet9.Tools.Web.Models;

public class CardItem
{
    public CardItem(string? firstTitle = null, string? firstUrl = null, string? secondTitle = null,
        string? secondUrl = null, string? cover = null)
    {
        FirstTitle = firstTitle;
        FirstUrl = firstUrl;
        SecondTitle = secondTitle;
        SecondUrl = secondUrl;
        Cover = cover;
    }

    public string? FirstTitle { get; }
    public string? FirstUrl { get; }
    public string? SecondTitle { get; }
    public string? SecondUrl { get; }
    public string? Cover { get; }
}