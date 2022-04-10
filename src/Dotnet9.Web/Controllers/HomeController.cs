using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using AutoMapper;
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
using Dotnet9.Domain.Repositories;
using Dotnet9.Domain.Shared.Blogs;
using Dotnet9.Domain.Tags;
using Dotnet9.Domain.Timelines;
using Dotnet9.Domain.UrlLinks;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Dotnet9.Web.Caches;
using Dotnet9.Web.Models;
using Dotnet9.Web.Utils;
using Dotnet9.Web.ViewModels.Abouts;
using Dotnet9.Web.ViewModels.Blogs;
using Dotnet9.Web.ViewModels.Homes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dotnet9.Web.Controllers;

public class HomeController : Controller
{
    private readonly IAlbumAppService _albumAppService;
    private readonly AlbumManager _albumManager;
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly BlogPostManager _blogPostManager;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICacheService _cacheService;
    private readonly ICategoryAppService _categoryAppService;
    private readonly CategoryManager _categoryManager;
    private readonly Dotnet9DbContext _dotnet9DbContext;
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly TagManager _tagManager;
    private readonly ITagRepository _tagRepository;
    private readonly UrlLinkManager _urlLinkManager;

    public HomeController(
        ILogger<HomeController> logger,
        Dotnet9DbContext dotnet9DbContext,
        IAlbumAppService albumAppService,
        AlbumManager albumManager,
        ICategoryAppService categoryAppService,
        CategoryManager categoryManager,
        ITagRepository tagRepository,
        TagManager tagManager,
        IBlogPostAppService blogPostAppService,
        IBlogPostRepository blogPostRepository,
        BlogPostManager blogPostManager,
        UrlLinkManager urlLinkManager,
        ICacheService cacheService,
        IMapper mapper)
    {
        _logger = logger;
        _dotnet9DbContext = dotnet9DbContext;
        _albumAppService = albumAppService;
        _albumManager = albumManager;
        _categoryAppService = categoryAppService;
        _categoryManager = categoryManager;
        _tagRepository = tagRepository;
        _tagManager = tagManager;
        _blogPostAppService = blogPostAppService;
        _blogPostRepository = blogPostRepository;
        _blogPostManager = blogPostManager;
        _urlLinkManager = urlLinkManager;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        const string cacheKey = $"{nameof(HomeController)}-{nameof(Index)}";
        var cacheData = await _cacheService.GetAsync<HomeViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new HomeViewModel();
        var recommend = await _blogPostRepository.SelectBlogPostBriefAsync(8, 1, x => x.InBanner, x => x.CreateDate,
            SortDirectionKind.Descending);
        cacheData.BlogPostsForRecommend =
            _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(recommend.Item1);
        cacheData.LoadMoreKinds = new Dictionary<string, LoadMoreKind>
        {
            {"最新", LoadMoreKind.Latest},
            {".NET", LoadMoreKind.Dotnet},
            {"大前端", LoadMoreKind.Front},
            {"数据库", LoadMoreKind.Database},
            {"更多语言", LoadMoreKind.MoreLanguage},
            {"课程", LoadMoreKind.Course},
            {"其他", LoadMoreKind.Other}
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }

    [HttpGet]
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

        var bytes = await _cacheService.GetAsync<byte[]>(cacheKey);
        if (bytes is {Length: > 0}) return File(bytes, contentType);

        var siteMapNodes = new List<SitemapNode>();

        siteMapNodes.AddRange((await _albumAppService.GetListCountAsync()).Select(x => new SitemapNode
        {
            LastModified = DateTime.UtcNow,
            Priority = 0.8,
            Url = $"{GlobalVar.SiteDomain}/album/{x.Slug}",
            Frequency = SitemapFrequency.Monthly
        }));

        siteMapNodes.AddRange((await _categoryAppService.ListAllAsync()).Select(x => new SitemapNode
        {
            LastModified = DateTime.UtcNow, Priority = 0.8, Url = $"{GlobalVar.SiteDomain}/cat/{x.Slug}",
            Frequency = SitemapFrequency.Monthly
        }));

        siteMapNodes.AddRange((await GetAllBlogPostForSitemap()).Select(x =>
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

        foreach (var m in siteMapNodes)
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

        await _cacheService.AddAsync(cacheKey, bytes, TimeSpan.FromHours(24));
        return File(bytes, contentType);
    }

    [HttpGet]
    [Route("archive")]
    public async Task<IActionResult> Archive()
    {
        var vm = new ArchiveViewModel
        {
            Items = await GetAllBlogPostForSitemap()
        };

        return View(vm);
    }

    public async Task<List<BlogPostForSitemap>> GetAllBlogPostForSitemap()
    {
        var cacheKey = $"{nameof(BlogPostController)}-{nameof(GetAllBlogPostForSitemap)}";
        var cacheData = await _cacheService.GetAsync<List<BlogPostForSitemap>>(cacheKey);
        if (cacheData != null) return cacheData;

        cacheData = await _blogPostAppService.GetListBlogPostForSitemapAsync();

        await _cacheService.ReplaceAsync(cacheKey, cacheData);

        return cacheData;
    }

    [HttpGet]
    [Route("seed")]
    public async Task<bool> Seed()
    {
        await SeedAlbums();

        await SeedCategory();

        await SeedBlogPost();

        await SeedUrlLink();

        await SeedAbout();

        await SeedDonation();

        await SeedTimeline();

        await SeedPrivacy();

        return true;
    }

    private async Task SeedPrivacy()
    {
        if (await _dotnet9DbContext.Privacies!.CountAsync() <= 0)
        {
            var filePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "Privacy.md");
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                var privacy = new Privacy {Content = fileContent};
                await _dotnet9DbContext.Privacies!.AddAsync(privacy);
                await _dotnet9DbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedTimeline()
    {
        if (await _dotnet9DbContext.Timelines!.CountAsync() <= 0)
        {
            var filePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "timelines.json");
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                var timelinesFromFile = JsonConvert.DeserializeObject<List<Timeline>>(fileContent)!;
                if (timelinesFromFile != null && timelinesFromFile.Any())
                {
                    await _dotnet9DbContext.Timelines!.AddRangeAsync(timelinesFromFile);
                    await _dotnet9DbContext.SaveChangesAsync();
                }
            }
        }
    }

    private async Task SeedDonation()
    {
        if (await _dotnet9DbContext.Donations!.CountAsync() <= 0)
        {
            var filePath = Path.Combine(GlobalVar.AssetsLocalPath!, "pays", "Donation.md");
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                var donation = new Donation {Content = fileContent};
                await _dotnet9DbContext.Donations!.AddAsync(donation);
                await _dotnet9DbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedAbout()
    {
        if (await _dotnet9DbContext.Abouts!.CountAsync() <= 0)
        {
            var filePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "about.md");
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                var about = new About {Content = fileContent};
                await _dotnet9DbContext.Abouts!.AddAsync(about);
                await _dotnet9DbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedUrlLink()
    {
        if (await _dotnet9DbContext.UrlLinks!.CountAsync() <= 0)
        {
            var filePath = Path.Combine(GlobalVar.AssetsLocalPath!, "site", "link.json");
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                var urlLinksFromFile = JsonConvert.DeserializeObject<List<UrlLinkDto>>(fileContent)!;
                var i = 1;
                var urlLinks = urlLinksFromFile?.Select(x =>
                        _urlLinkManager.CreateAsync(i++, x.Index, (UrlLinkKind) Enum.Parse(typeof(UrlLinkKind), x.Kind),
                            x.Name, x.Description, x.Url).Result)
                    .ToList();
                if (urlLinks != null && urlLinks.Any())
                {
                    await _dotnet9DbContext.UrlLinks!.AddRangeAsync(urlLinks);
                    await _dotnet9DbContext.SaveChangesAsync();
                }
            }
        }
    }

    private async Task SeedBlogPost()
    {
        if (await _dotnet9DbContext.BlogPosts!.CountAsync() <= 0)
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
                            await _dotnet9DbContext.Tags!.AddAsync(existTag);
                            await _dotnet9DbContext.SaveChangesAsync();
                        }
                        catch
                        {
                            // ignored
                        }

                try
                {
                    await _dotnet9DbContext.BlogPosts!.AddAsync(await _blogPostManager.CreateAsync(
                        blogPostSeed.Title,
                        blogPostSeed.Slug,
                        blogPostSeed.BriefDescription,
                        blogPostSeed.InBanner,
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
                    await _dotnet9DbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"做BlogPost种子异常：{ex}");
                }
            }
        }
    }

    private async Task SeedCategory()
    {
        if (await _dotnet9DbContext.Categories!.CountAsync() <= 0)
        {
            var filePath = Path.Combine(GlobalVar.AssetsLocalPath!, "cats", "category.json");
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                var categoriesFromFile = JsonConvert.DeserializeObject<List<CategoryItem>>(fileContent)!;
                var i = 1;
                var categoriesToDb = new List<Category>();
                foreach (var child in categoriesFromFile)
                {
                    ReadCategory(categoriesToDb, child, ref i, -1);
                    i++;
                }

                if (categoriesToDb.Any())
                {
                    await _dotnet9DbContext.Categories!.AddRangeAsync(categoriesToDb);
                    await _dotnet9DbContext.SaveChangesAsync();
                }
            }
        }
    }

    private async Task SeedAlbums()
    {
        if (await _dotnet9DbContext.Albums!.CountAsync() <= 0)
        {
            var filePath = Path.Combine(GlobalVar.AssetsLocalPath!, "albums", "album.json");
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                var albumsFromFile = JsonConvert.DeserializeObject<List<AlbumItem>>(fileContent);
                var i = 1;
                var albums = albumsFromFile?.Select(x => _albumManager.CreateAsync(i++, x.Name, x.Slug,
                        Path.Combine(GlobalVar.AssetsRemotePath!, x.Cover), null).Result
                    )
                    .ToList();
                if (albums != null && albums.Any())
                {
                    await _dotnet9DbContext.Albums!.AddRangeAsync(albums);
                    await _dotnet9DbContext.SaveChangesAsync();
                }
            }
        }
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
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
}