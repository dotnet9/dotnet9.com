using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Core;
using Dotnet9.Domain.Abouts;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Donations;
using Dotnet9.Domain.Privacies;
using Dotnet9.Domain.Shared.Blogs;
using Dotnet9.Domain.Tags;
using Dotnet9.Domain.Timelines;
using Dotnet9.Domain.UrlLinks;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Dotnet9.Web.Models;
using Dotnet9.Web.Utils;
using Dotnet9.Web.ViewModels.Abouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Dotnet9.Web.Controllers;

public class HomeController : Controller
{
    private readonly IAlbumAppService _albumAppService;
    private readonly AlbumManager _albumManager;
    private readonly IAlbumRepository _albumRepository;
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly BlogPostManager _blogPostManager;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryAppService _categoryAppService;
    private readonly CategoryManager _categoryManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly Dotnet9DbContext _Dotnet9DbContext;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<HomeController> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly TagManager _tagManager;
    private readonly ITagRepository _tagRepository;
    private readonly UrlLinkManager _urlLinkManager;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public HomeController(
        ILogger<HomeController> logger,
        Dotnet9DbContext Dotnet9DbContext,
        IAlbumAppService albumAppService,
        IAlbumRepository albumRepository,
        AlbumManager albumManager,
        ICategoryAppService categoryAppService,
        ICategoryRepository categoryRepository,
        CategoryManager categoryManager,
        ITagRepository tagRepository,
        TagManager tagManager,
        IBlogPostAppService blogPostAppService,
        IBlogPostRepository blogPostRepository,
        BlogPostManager blogPostManager,
        IUrlLinkRepository urlLinkRepository,
        UrlLinkManager urlLinkManager,
        IHostEnvironment hostEnvironment,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _Dotnet9DbContext = Dotnet9DbContext;
        _albumAppService = albumAppService;
        _albumRepository = albumRepository;
        _albumManager = albumManager;
        _categoryAppService = categoryAppService;
        _categoryRepository = categoryRepository;
        _categoryManager = categoryManager;
        _tagRepository = tagRepository;
        _tagManager = tagManager;
        _blogPostAppService = blogPostAppService;
        _blogPostRepository = blogPostRepository;
        _blogPostManager = blogPostManager;
        _urlLinkRepository = urlLinkRepository;
        _urlLinkManager = urlLinkManager;
        _hostEnvironment = hostEnvironment;
        _memoryCache = memoryCache;
    }

    public async Task<IActionResult> Index()
    {
        return await Task.FromResult(View());
    }

    [Route("/sitemap.xml")]
    public async Task<IActionResult> Sitemap()
    {
        const string contentType = "application/xml";
        const string cacheKey = "sitemap.xml";
        var cd = new ContentDisposition
        {
            FileName = cacheKey,
            Inline = true
        };
        Response.Headers.Append("Content-Disposition", cd.ToString());

        var bytes = _memoryCache.Get<byte[]>(cacheKey);
        if (bytes != null) return File(bytes, contentType);

        var sitemapNodes = new List<SitemapNode>();

        sitemapNodes.AddRange((await _albumAppService.GetListCountAsync()).Select(x => new SitemapNode
        {
            LastModified = DateTime.UtcNow,
            Priority = 0.8,
            Url = $"{GlobalVar.SiteDomain}/album/{x.Slug}",
            Frequency = SitemapFrequency.Monthly
        }));

        sitemapNodes.AddRange((await _categoryAppService.ListAllAsync()).Select(x => new SitemapNode
        {
            LastModified = DateTime.UtcNow, Priority = 0.8, Url = $"{GlobalVar.SiteDomain}/cat/{x.Slug}",
            Frequency = SitemapFrequency.Monthly
        }));

        sitemapNodes.AddRange((await _blogPostAppService.GetListBlogPostForSitemap()).Select(x =>
            new SitemapNode
            {
                LastModified = x.CreateDate, Priority = 0.9,
                Url =
                    $"{GlobalVar.SiteDomain}/{x.CreateDate.ToString("yyyy")}/{x.CreateDate.ToString("MM")}/{x.Slug}",
                Frequency = SitemapFrequency.Daily
            }));

        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
        sb.AppendLine("   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
        sb.AppendLine(
            "   xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

        foreach (var m in sitemapNodes)
        {
            sb.AppendLine("    <url>");

            sb.AppendLine($"        <loc>{m.Url}</loc>");
            sb.AppendLine($"        <lastmod>{m.LastModified.ToString("yyyy-MM-dd")}</lastmod>");
            sb.AppendLine($"        <changefreq>{m.Frequency}</changefreq>");
            sb.AppendLine($"        <priority>{m.Priority}</priority>");

            sb.AppendLine("    </url>");
        }

        sb.AppendLine("</urlset>");

        bytes = Encoding.UTF8.GetBytes(sb.ToString());

        _memoryCache.Set(cacheKey, bytes, TimeSpan.FromHours(24));
        return File(bytes, contentType);
    }

    [Route("seed")]
    public async Task<bool> Seed()
    {
        if (await _Dotnet9DbContext.Albums!.CountAsync() <= 0)
        {
            var albumJsonFilePath = Path.Combine(GlobalVar.AssetsLocalPath!, "albums", "album.json");
            if (System.IO.File.Exists(albumJsonFilePath))
            {
                var albumJsonString = await System.IO.File.ReadAllTextAsync(albumJsonFilePath);
                var albumsFromFile = JsonConvert.DeserializeObject<List<AlbumItem>>(albumJsonString);
                var i = 1;
                var albums = albumsFromFile?.Select(x => _albumManager.CreateAsync(i++, x.Name, x.Slug,
                        Path.Combine(GlobalVar.AssetsRemotePath!, x.Cover), null).Result
                    )
                    .ToList();
                if (albums != null && albums.Any())
                {
                    await _Dotnet9DbContext.Albums!.AddRangeAsync(albums);
                    await _Dotnet9DbContext.SaveChangesAsync();
                }
            }
        }

        if (await _Dotnet9DbContext.Categories!.CountAsync() <= 0)
        {
            var categoryJsonFilePath = Path.Combine(GlobalVar.AssetsLocalPath!, "cats", "category.json");
            if (System.IO.File.Exists(categoryJsonFilePath))
            {
                var categoryJsonString = await System.IO.File.ReadAllTextAsync(categoryJsonFilePath);
                var categoriesFromFile = JsonConvert.DeserializeObject<List<CategoryItem>>(categoryJsonString)!;
                var i = 1;
                var categoriesToDb = new List<Category>();
                foreach (var child in categoriesFromFile)
                {
                    ReadCategory(categoriesToDb, child, ref i, -1);
                    i++;
                }

                if (categoriesToDb.Any())
                {
                    await _Dotnet9DbContext.Categories!.AddRangeAsync(categoriesToDb);
                    await _Dotnet9DbContext.SaveChangesAsync();
                }
            }
        }

        if (await _Dotnet9DbContext.BlogPosts!.CountAsync() <= 0)
        {
            var blogPostFiles = Directory.GetFiles(GlobalVar.AssetsLocalPath!, "*.info", SearchOption.AllDirectories);
            foreach (var blogPostFile in blogPostFiles)
            {
                var blogPostSeed =
                    JsonConvert.DeserializeObject<BlogPostItem>(await System.IO.File.ReadAllTextAsync(blogPostFile))!;
                blogPostSeed.Content = await System.IO.File.ReadAllTextAsync(blogPostFile.Replace(".info", ".md"));
                if (blogPostSeed.BriefDescription.IsNullOrWhiteSpace())
                {
                    if (blogPostSeed.Content.Length < BlogPostConsts.MaxBriefDescriptionLength)
                        blogPostSeed.BriefDescription = blogPostSeed.Content;
                    else
                        blogPostSeed.BriefDescription =
                            blogPostSeed.Content.Substring(0, BlogPostConsts.MaxBriefDescriptionLength - 5) + "...";
                }

                if (blogPostSeed.Tags != null && blogPostSeed.Tags.Any())
                    foreach (var tagName in blogPostSeed.Tags)
                        try
                        {
                            var existTag = await _tagRepository.FindByNameAsync(tagName);
                            if (existTag != null) continue;

                            existTag = await _tagManager.CreateAsync(null, tagName);
                            await _Dotnet9DbContext.Tags!.AddAsync(existTag);
                            await _Dotnet9DbContext.SaveChangesAsync();
                        }
                        catch
                        {
                            // ignored
                        }

                try
                {
                    await _Dotnet9DbContext.BlogPosts!.AddAsync(await _blogPostManager.CreateAsync(
                        blogPostSeed.Title,
                        blogPostSeed.Slug,
                        blogPostSeed.BriefDescription,
                        blogPostSeed.Cover,
                        blogPostSeed.Content,
                        blogPostSeed.CopyrightType!.Value,
                        blogPostSeed.Original,
                        null,
                        blogPostSeed.OriginalTitle,
                        blogPostSeed.OriginalLink,
                        blogPostSeed.Albums,
                        blogPostSeed.Categories,
                        blogPostSeed.Tags,
                        DateTime.Parse(blogPostSeed.CreateDate!)));
                    await _Dotnet9DbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"做BlogPost种子异常：{ex}");
                }
            }
        }

        if (await _Dotnet9DbContext.UrlLinks!.CountAsync() <= 0)
        {
            var urlLinkJsonFilePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "link.json");
            if (System.IO.File.Exists(urlLinkJsonFilePath))
            {
                var urlLinkJsonString = await System.IO.File.ReadAllTextAsync(urlLinkJsonFilePath);
                var urlLinksFromFile = JsonConvert.DeserializeObject<List<UrlLinkDto>>(urlLinkJsonString)!;
                var i = 1;
                var urlLinks = urlLinksFromFile?.Select(x =>
                        _urlLinkManager.CreateAsync(i++, x.Index, (UrlLinkKind) Enum.Parse(typeof(UrlLinkKind), x.Kind),
                            x.Name, x.Description, x.Url).Result)
                    .ToList();
                if (urlLinks != null && urlLinks.Any())
                {
                    await _Dotnet9DbContext.UrlLinks!.AddRangeAsync(urlLinks);
                    await _Dotnet9DbContext.SaveChangesAsync();
                }
            }
        }

        if (await _Dotnet9DbContext.Abouts!.CountAsync() <= 0)
        {
            var aboutMakrdownFilePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "about.md");
            if (System.IO.File.Exists(aboutMakrdownFilePath))
            {
                var aboutMarkdownString = await System.IO.File.ReadAllTextAsync(aboutMakrdownFilePath);
                var about = new About {Content = aboutMarkdownString};
                await _Dotnet9DbContext.Abouts!.AddAsync(about);
                await _Dotnet9DbContext.SaveChangesAsync();
            }
        }

        if (await _Dotnet9DbContext.Donations!.CountAsync() <= 0)
        {
            var donationMakrdownFilePath = Path.Combine(GlobalVar.AssetsLocalPath!, "pays", "Donation.md");
            if (System.IO.File.Exists(donationMakrdownFilePath))
            {
                var donationMarkdownString = await System.IO.File.ReadAllTextAsync(donationMakrdownFilePath);
                var donation = new Donation {Content = donationMarkdownString};
                await _Dotnet9DbContext.Donations!.AddAsync(donation);
                await _Dotnet9DbContext.SaveChangesAsync();
            }
        }

        if (await _Dotnet9DbContext.Timelines!.CountAsync() <= 0)
        {
            var timelinesJsonFilePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "timelines.json");
            if (System.IO.File.Exists(timelinesJsonFilePath))
            {
                var timelinesJsonString = await System.IO.File.ReadAllTextAsync(timelinesJsonFilePath);
                var timelinesFromFile = JsonConvert.DeserializeObject<List<Timeline>>(timelinesJsonString)!;
                if (timelinesFromFile != null && timelinesFromFile.Any())
                {
                    await _Dotnet9DbContext.Timelines!.AddRangeAsync(timelinesFromFile);
                    await _Dotnet9DbContext.SaveChangesAsync();
                }
            }
        }

        if (await _Dotnet9DbContext.Privacies!.CountAsync() <= 0)
        {
            var privacyMakrdownFilePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "Privacy.md");
            if (System.IO.File.Exists(privacyMakrdownFilePath))
            {
                var privacyMarkdownString = await System.IO.File.ReadAllTextAsync(privacyMakrdownFilePath);
                var privacy = new Privacy() { Content = privacyMarkdownString };
                await _Dotnet9DbContext.Privacies!.AddAsync(privacy);
                await _Dotnet9DbContext.SaveChangesAsync();
            }
        }

        return true;
    }

    private void ReadCategory(List<Category> container, CategoryItem categoryFromFile, ref int id,
        int parentId)
    {
        var currentId = id;
        var category = _categoryManager.CreateAsync(currentId, categoryFromFile.Name, categoryFromFile.Slug,
            Path.Combine(GlobalVar.AssetsRemotePath!, categoryFromFile.Cover), null, parentId).Result;
        container.Add(category);

        if (categoryFromFile.Children is not {Count: > 0}) return;
        foreach (var child in categoryFromFile.Children)
        {
            id++;
            ReadCategory(container, child, ref id, currentId);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}