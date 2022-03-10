namespace Dotnet9.Tools.Web.Utils;

public static class SitePathHelper
{
    public const string PublicSourceUrl =
        "https://github.com/dotnet9/dotnet9.com/blob/develop/src/Dotnet9.Tools.Web/Pages/Public";

    public static string ToolsIconMarkdown = Path.Combine(UploadsPath, "2022", "02", "2022-02-22_01.md");
    public static string ToolsIconMainPostUrl = "https://dotnet9.com/1715";

    public static string ToolsIconSourceUrl = $"{PublicSourceUrl}/Tools/ImageTools/IcoTool.razor";
    public static string ToolsTimestampMarkdown = Path.Combine(UploadsPath, "2022", "02", "2022-02-27_03.md");
    public static string ToolsTimestampMainPostUrl = "https://dotnet9.com/1801";

    public static string ToolsTimestampSourceUrl = $"{PublicSourceUrl}/Tools/TimeTools/TimestampTool.razor";
    public static string WwwrootPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
    public static string DocPath => Path.Combine(WwwrootPath, "doc");
    public static string ToolPath => Path.Combine(WwwrootPath, "tool.json");
    public static string SitemapPath => Path.Combine(WwwrootPath, "sitemap.xml");
    public static string BlogContentsPath => Path.Combine(DocPath, "blog_contents");
    public static string TimelinePath => Path.Combine(DocPath, "timelines.json");
    public static string ReadmePath => Path.Combine(DocPath, "README-zh_CN.md");
    public static string AboutPath => Path.Combine(BlogContentsPath, "about.md");
    public static string AlbumPath => Path.Combine(BlogContentsPath, "album.json");
    public static string CategoryPath => Path.Combine(BlogContentsPath, "category.json");
    public static string UploadsPath => Path.Combine(BlogContentsPath, "uploads");
}