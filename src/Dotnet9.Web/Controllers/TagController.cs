using System.Net;
using Dotnet9.Application.Contracts.Tags;
using Dotnet9.Core;
using Dotnet9.Web.ViewModels.Tags;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class TagController : Controller
{
    private readonly ITagAppService _tagAppService;

    public TagController(ITagAppService tagAppService)
    {
        _tagAppService = tagAppService;
    }

    [Route("tag/{name?}")]
    public async Task<IActionResult> Index(string? name)
    {
        var vm = new TagViewModel();
        if (name.IsNullOrWhiteSpace())
        {
            vm.Tags = await _tagAppService.GetListCountAsync();
        }
        else
        {
            var factName = WebUtility.UrlDecode(name);
            vm.TagName = name;
            vm.BlogPosts = await _tagAppService.GetBlogPostListAsync(factName);
        }

        return View(vm);
    }
}