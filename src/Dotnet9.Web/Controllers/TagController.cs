using System.Net;
using Dotnet9.Application.Contracts.Tags;
using Dotnet9.Core;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ViewModels.Tags;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class TagController : Controller
{
    private readonly ICacheService _cacheService;
    private readonly ITagAppService _tagAppService;

    public TagController(ITagAppService tagAppService, ICacheService cacheService)
    {
        _tagAppService = tagAppService;
        _cacheService = cacheService;
    }

    [Route("tag/{name?}")]
    public async Task<IActionResult> Index(string? name)
    {
        var cacheKey = $"{nameof(TagController)}-{nameof(Index)}-{name}";
        var cacheData = await _cacheService.GetAsync<TagViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new TagViewModel();
        if (name.IsNullOrWhiteSpace())
        {
            cacheData.Tags = await _tagAppService.GetListCountAsync();
        }
        else
        {
            var factName = WebUtility.UrlDecode(name);
            cacheData.TagName = name;
            cacheData.BlogPosts = await _tagAppService.GetBlogPostListAsync(factName!);
        }

        await _cacheService.ReplaceAsync(cacheKey, cacheData);

        return View(cacheData);
    }
}