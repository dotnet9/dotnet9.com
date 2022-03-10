namespace Dotnet9.Tools.Web.Models;

public class FriendLink
{
    public FriendLink(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public string Name { get; set; }

    public string Url { get; set; }
}