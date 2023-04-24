namespace Dotnet9.RazorPages.Pages;

public class SitemapModel : PageModel
{
    public async Task<IActionResult> OnGet([FromServices] ICaller caller, [FromServices] ISystemService systemService)
    {
        var siteInfo = await systemService.GetSiteInfoAsync();
        var dataFromServer = await caller.GetAsync<SitemapInfo?>($"/api/systems/sitemap");

        byte[] GetSitemap()
        {
            List<SitemapNode> siteMapNodes = new();

            siteMapNodes.AddRange(dataFromServer!.AlbumSlugs.Select(slug => new SitemapNode
            {
                LastModified = DateTimeOffset.UtcNow,
                Priority = 0.8,
                Url = $"{siteInfo?.Domain}/album/{slug}",
                Frequency = SitemapFrequency.Monthly
            }).ToList());

            siteMapNodes.AddRange(dataFromServer.CategorySlugs.Select(slug => new SitemapNode
            {
                LastModified = DateTimeOffset.UtcNow,
                Priority = 0.8,
                Url = $"{siteInfo?.Domain}/cat/{slug}",
                Frequency = SitemapFrequency.Monthly
            }).ToList());

            siteMapNodes.AddRange(dataFromServer.Blogs.Select(blog =>
                new SitemapNode
                {
                    LastModified = blog.Value,
                    Priority = 0.9,
                    Url =
                        $"{siteInfo?.Domain}/{blog.Value:yyyy/MM}/{blog.Key}",
                    Frequency = SitemapFrequency.Daily
                }).ToList());

            StringBuilder sb = new();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            sb.AppendLine("   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            sb.AppendLine(
                "   xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

            foreach (SitemapNode m in siteMapNodes)
            {
                sb.AppendLine("    <url>");

                sb.AppendLine($"        <loc>{m.Url}</loc>");
                sb.AppendLine($"        <lastmod>{m.LastModified.ToString("yyyy-MM-dd")}</lastmod>");
                sb.AppendLine($"        <changefreq>{m.Frequency}</changefreq>");
                sb.AppendLine($"        <priority>{m.Priority}</priority>");

                sb.AppendLine("    </url>");
            }

            sb.AppendLine("</urlset>");

            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());

            return bytes;
        }

        ContentDisposition cd = new()
        {
            FileName = "sitemap.xml",
            Inline = true
        };
        Response.Headers.Append("Content-Disposition", cd.ToString());

        var siteMapData = GetSitemap();

        return File(siteMapData, "application/xml");
    }
}

public class SitemapNode
{
    public string Url { get; set; } = null!;
    public DateTimeOffset LastModified { get; set; }
    public SitemapFrequency Frequency { get; set; }
    public double Priority { get; set; }
}

public enum SitemapFrequency
{
    Monthly,
    Daily
}