using Dotnet9.Tools.Web.Models;
using System.Text;

namespace Dotnet9.Tools.Web.Utils;

public static class SitemapHelper
{
    private const string UtcTimeFormat = "yyyy-MM-dd HH:mm:ss+08:00";

    public static string GenerateSiteMapString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine($"<!-- generated-on=\"{DateTimeOffset.UtcNow.ToString(UtcTimeFormat)}\" -->");
        sb.AppendLine("<!-- generator=\"XML Sitemap & Dotnet9 Tools\" -->");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        var pages = new List<PageInfo>();
        pages.Add(new PageInfo(SiteUrlHelper.BaseUrl));
        pages.Add(new PageInfo(SiteUrlHelper.ToolsIndexFullUrl,
            new DateTimeOffset(2022, 02, 28, 0, 0, 0, TimeSpan.Zero)));
        pages.Add(
            new PageInfo(SiteUrlHelper.ToolsIconFullUrl, new DateTimeOffset(2022, 02, 22, 0, 0, 0, TimeSpan.Zero)));
        pages.Add(new PageInfo(SiteUrlHelper.ToolsTimestampFullUrl,
            new DateTimeOffset(2022, 02, 28, 0, 0, 0, TimeSpan.Zero)));
        pages.Add(new PageInfo(SiteUrlHelper.AlbumFullUrl));
        pages.AddRange(ConstData.AlbumTreeItems.Select(x => new PageInfo(x.Slug.GetAlbumFullUrl())));
        pages.Add(new PageInfo(SiteUrlHelper.CategoryFullUrl));
        pages.AddRange(ConstData.CategoryNotTreeItems.Select(x => new PageInfo(x.Slug.GetCategoryFullUrl())));
        pages.AddRange(ConstData.BlogPostItems.Select(x =>
            new PageInfo(x.Slug!.GetPostFullUrl(), DateTime.Parse(x.UpdateDate ?? x.CreateDate!))));
        foreach (var pi in pages)
        {
            sb.AppendLine("<url>");
            sb.AppendLine($"<loc>{pi.Url}</loc>");
            sb.AppendLine($"<lastmod>{pi.LastModifyDateTime.ToString(UtcTimeFormat)}</lastmod>");
            sb.AppendLine("</url>");
        }

        sb.AppendLine("</urlset>");
        return sb.ToString();
    }

    public static void CreateSiteMap(string filePath)
    {
        if (File.Exists(filePath)) File.Delete(filePath);

        File.WriteAllText(filePath, GenerateSiteMapString(), Encoding.UTF8);
    }
}

public class PageInfo
{
    public PageInfo(string url, DateTimeOffset? lastModifyDateTime = null)
    {
        Url = url;
        LastModifyDateTime = lastModifyDateTime ?? DateTimeOffset.UtcNow;
    }

    public string? Url { get; set; }
    public DateTimeOffset LastModifyDateTime { get; set; }
}