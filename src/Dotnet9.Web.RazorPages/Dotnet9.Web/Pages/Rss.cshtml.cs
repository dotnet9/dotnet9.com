namespace Dotnet9.Web.Pages;

public class RssModel : PageModel
{
    private readonly IBlogPostService _service;
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly IOptionsSnapshot<SiteOptions> _siteOptions;

    public RssModel(IBlogPostService service,
        IDistributedCacheHelper cacheHelper, IOptionsSnapshot<SiteOptions> siteOptions)
    {
        _service = service;
        _cacheHelper = cacheHelper;
        _siteOptions = siteOptions;
    }

    public async Task<IActionResult> OnGet()
    {
        string cacheKey = "Rss";

        async Task<string?> GetDataFromDb()
        {
            var data = await _service.GetBlogPostBriefListAsync(new GetBlogPostBriefListRequest(null, 1, 10));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine(
                "<rss xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\" version=\"2.0\">");
            sb.Append("<channel>");
            sb.Append(
                $"<atom:link rel=\"self\" type=\"application/rss+xml\" href=\"{_siteOptions.Value.Domain}/rss\"/>");
            sb.Append($"<title>{_siteOptions.Value.AppName}_{_siteOptions.Value.Subheading}</title>");
            sb.Append($"<link>{_siteOptions.Value.Domain}/rss</link>");
            sb.Append($"<description>{_siteOptions.Value.Description}</description>");
            sb.Append($"<copyright>{_siteOptions.Value.AppName}_{_siteOptions.Value.Subheading}</copyright>");
            sb.Append("<language>zh-cn</language>");
            if (data.Data is { Count: > 0 })
            {
                foreach (var item in data.Data)
                {
                    sb.Append("<item>");
                    sb.Append($"<title>{item.Title}</title>");
                    sb.Append(
                        $"<link>{_siteOptions.Value.Domain}/{item.CreationTime.ToString("yyyy/MM")}/{item.Slug}</link>");
                    sb.Append($"<description>{item.Description}</description>");
                    sb.Append($"<author>({item.Original ?? _siteOptions.Value.Owner})</author>");
                    sb.Append($"<category>{item.Categories.Select(x => x.Name).JoinAsString(",")}</category>");
                    sb.Append(
                        $"<guid>{_siteOptions.Value.Domain}/{item.CreationTime.ToString("yyyy/MM")}/{item.Slug}</guid>");
                    sb.Append($"<pubDate>{item.CreationTime:R}</pubDate>");
                    sb.Append($"<content:encoded><![CDATA[{item.Description}]]></content:encoded>");
                    sb.Append("</item>");
                }
            }

            sb.Append("</channel>");
            sb.AppendLine("</rss>");

            return sb.ToString();
        }

        var rss = await _cacheHelper.GetOrCreateAsync(cacheKey, async e => await GetDataFromDb());
        return Content(rss ?? "", "application/xml");
    }
}