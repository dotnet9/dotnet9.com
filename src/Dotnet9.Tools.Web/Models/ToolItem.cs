namespace Dotnet9.Tools.Web.Models;

public class ToolItem
{
    public ToolItem(string name, string url)
    {
        Name = name;
        Url = url;
        Children = new List<ToolItem>();
    }

    public string Name { get; set; }
    public string Url { get; set; }
    public List<ToolItem>? Children { get; set; }
}