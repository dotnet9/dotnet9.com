namespace Dotnet9.Tools.Web.Models;

public class ItemInfo
{
    public ItemInfo(string title, string desc, string cover, string url)
    {
        Title = title;
        Description = desc;
        Cover = cover;
        Url = url;
    }

    public string Title { get; }
    public string Description { get; }
    public string Cover { get; }
    public string Url { get; }
}