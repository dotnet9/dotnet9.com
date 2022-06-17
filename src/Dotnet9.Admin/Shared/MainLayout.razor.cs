namespace Dotnet9.Admin.Shared;

public partial class MainLayout
{
    private string _color = "purple darken-3";
    private List<(string Text, string Value)> _colors = new()
    {
        new("pink", "purple darken-1"),
        new("indigo", "indigo"),
        new("teal", "teal"),
        new("deep-purple", "deep-purple darken-1"),
        new("yellow", "yellow darken-4"),
    };
}