namespace Dotnet9.RazorPages.Pages;

public class RssModel : PageModel
{
    public async Task<IActionResult> OnGet([FromServices] ICaller caller,
        [FromServices] ISystemService systemService)
    {
        var siteInfo = await systemService.GetSiteInfoAsync();
        var getBlogResult = await caller.GetAsync<GetBlogListByKeywordsResponse>("/api/blogs");

        string GetRss()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine(
                "<rss xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\" version=\"2.0\">");
            sb.Append("<channel>");
            sb.Append(
                $"<atom:link rel=\"self\" type=\"application/rss+xml\" href=\"{siteInfo.Domain}/rss\"/>");
            sb.Append($"<title>{siteInfo.AppName}_{siteInfo.Subheading}</title>");
            sb.Append($"<link>{siteInfo.Domain}/rss</link>");
            sb.Append($"<description>{siteInfo.Description}</description>");
            sb.Append($"<copyright>{siteInfo.AppName}_{siteInfo.Subheading}</copyright>");
            sb.Append("<language>zh-cn</language>");
            if (getBlogResult?.Records is { Count: > 0 })
            {
                foreach (var item in getBlogResult.Records)
                {
                    sb.Append("<item>");
                    sb.Append($"<title>{item.Title}</title>");
                    sb.Append(
                        $"<link>{siteInfo.Domain}/{item.CreationTime.ToString("yyyy/MM")}/{item.Slug}</link>");
                    sb.Append($"<description>{item.Description}</description>");
                    sb.Append($"<author>({item.Original ?? siteInfo.Owner})</author>");
                    sb.Append($"<category>{item.Categories?.Select(x => x.Name).JoinAsString(",")}</category>");
                    sb.Append(
                        $"<guid>{siteInfo.Domain}/{item.CreationTime.ToString("yyyy/MM")}/{item.Slug}</guid>");
                    sb.Append($"<pubDate>{item.CreationTime:R}</pubDate>");
                    sb.Append($"<content:encoded><![CDATA[{item.Description}]]></content:encoded>");
                    sb.Append("</item>");
                }
            }

            sb.Append("</channel>");
            sb.AppendLine("</rss>");

            return sb.ToString();
        }

        var rss = GetRss();
        return Content(rss, "application/xml");
    }
}