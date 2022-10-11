namespace Dotnet9.Web.Pages.Abouts;

public class SitemapModel : PageModel
{
    private readonly IDistributedCacheHelper _cacheHelper;
    public readonly Dotnet9DbContext _dbContext;
    private readonly IOptionsSnapshot<SiteOptions> _siteOptions;

    public SitemapModel(IOptionsSnapshot<SiteOptions> SiteOptions, Dotnet9DbContext dbContext,
        IDistributedCacheHelper cacheHelper)
    {
        _siteOptions = SiteOptions;
        _dbContext = dbContext;
        _cacheHelper = cacheHelper;
    }

    public async Task<IActionResult> OnGet()
    {
        const string contentType = "application/xml";
        const string cacheKey = "sitemap.xml";
        ContentDisposition cd = new ContentDisposition
        {
            FileName = cacheKey,
            Inline = true
        };
        Response.Headers.Append("Content-Disposition", cd.ToString());

        async Task<byte[]?> GetSitemap()
        {
            List<SitemapNode> siteMapNodes = new List<SitemapNode>();

            siteMapNodes.AddRange(await _dbContext.Albums!.Select(x => new SitemapNode
            {
                LastModified = DateTimeOffset.UtcNow,
                Priority = 0.8,
                Url = $"{_siteOptions.Value.Domain}/album/{x.Slug}",
                Frequency = SitemapFrequency.Monthly
            }).ToListAsync());

            siteMapNodes.AddRange(await _dbContext.Categories!.Select(x => new SitemapNode
            {
                LastModified = DateTimeOffset.UtcNow,
                Priority = 0.8,
                Url = $"{_siteOptions.Value.Domain}/cat/{x.Slug}",
                Frequency = SitemapFrequency.Monthly
            }).ToListAsync());

            siteMapNodes.AddRange(await _dbContext.BlogPosts!.Select(x =>
                new SitemapNode
                {
                    LastModified = x.CreationTime,
                    Priority = 0.9,
                    Url =
                        $"{_siteOptions.Value.Domain}/{x.CreationTime.ToString("yyyy")}/{x.CreationTime.ToString("MM")}/{x.Slug}",
                    Frequency = SitemapFrequency.Daily
                }).ToListAsync());

            StringBuilder sb = new StringBuilder();
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

        byte[]? siteMapData = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetSitemap());

        return File(siteMapData!, contentType);
    }
}