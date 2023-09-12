using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace Dotnet9Tools.Helper;

public static class HtmlHelper
{
    public static string? GetHtmlContent(string html, int len)
    {
        HtmlParser parser = new HtmlParser();
        IHtmlDocument doc = parser.ParseDocument(html);
        return doc.Body?.TextContent.RemoveEmpty();
    }
}