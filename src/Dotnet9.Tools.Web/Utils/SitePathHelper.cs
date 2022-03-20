namespace Dotnet9.Tools.Web.Utils;

public static class SitePathHelper
{
    private static string? _resourcePath;
    public static void SetResourcePath(string resourcePath)
    {
        _resourcePath = resourcePath;
    }
    public static string? ResourcePath { get { return _resourcePath; } }
    public const string PublicSourceUrl =
        "https://github.com/dotnet9/dotnet9.com/blob/develop/src/Dotnet9.Tools.Web/Pages/Public";

    public static string ToolsIconMarkdown = $@"{_resourcePath}\2022\02\2022-02-22_01.md";
    public static string ToolsIconMainPostUrl = "https://dotnet9.com/1715";

    public static string ToolsIconSourceUrl = $"{PublicSourceUrl}/Tools/ImageTools/IcoTool.razor";
    public static string ToolsTimestampMarkdown = $@"{_resourcePath}\2022\02\2022-02-27_03.md";
    public static string ToolsTimestampMainPostUrl = "https://dotnet9.com/1801";

    public static string ToolsTimestampSourceUrl = $"{PublicSourceUrl}/Tools/TimeTools/TimestampTool.razor";
    public static string ToolPath =>  $"{_resourcePath}\tools\tool.json";
    public static string SitemapPath => $@"{_resourcePath}\site\sitemap.xml";
    public static string TimelinePath => $@"{_resourcePath}\site\timelines.json";
    public static string AlbumPath => $@"{_resourcePath}\albums\album.json";
    public static string CategoryPath => $@"{_resourcePath}\cats\category.json";
}