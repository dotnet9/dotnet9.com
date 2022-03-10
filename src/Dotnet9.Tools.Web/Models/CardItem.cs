using Dotnet9.Tools.Web.Utils;

namespace Dotnet9.Tools.Web.Models;

public class CardItem
{
    public CardItem(string? firstTitle = null, string? firstUrl = null, string? secondTitle = null,
        string? secondUrl = null, string? cover = null, string? author = null, string? date = null,
        string? originalLink = null)
    {
        FirstTitle = firstTitle;
        FirstUrl = firstUrl;
        SecondTitle = secondTitle;
        SecondUrl = secondUrl;
        Cover = cover;
        Author = author;
        Date = date;
        OriginalLink = originalLink;
    }

    public string? FirstTitle { get; set; }
    public string? FirstUrl { get; set; }
    public string? SecondTitle { get; set; }
    public string? SecondUrl { get; set; }
    public string? Cover { get; set; }
    public string? Author { get; set; }
    public string? Date { get; set; }
    public string? OriginalLink { get; set; }
}

public static class CardItemExtension
{
    public static CardItem CreateCardItem(CategoryItem? category, BlogPost post)
    {
        var item = new CardItem
        {
            FirstTitle = category?.Name,
            FirstUrl = category?.Slug.GetCategoryUrl(),
            SecondTitle = post.Title,
            SecondUrl = post.Slug?.GetPostUrl(),
            Cover = post.Cover,
            Author = post.CopyrightType == CopyrightType.Default ? "沙漠尽头的狼" : post.Original,
            OriginalLink = post.CopyrightType == CopyrightType.Default ? "/" : post.OriginalLink,
            Date = post.CreateDate
        };

        return item;
    }
}