using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Core;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Dotnet9.Web.Controllers;

public class CategoryController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly ICacheService _cacheService;
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService, IBlogPostAppService blogPostAppService,
        ICacheService cacheService)
    {
        _categoryAppService = categoryAppService;
        _blogPostAppService = blogPostAppService;
        _cacheService = cacheService;
    }

    [HttpGet]
    [Route("cat/{slug?}")]
    public async Task<IActionResult> Index(string? slug)
    {
        var cacheKey = $"{nameof(CategoryController)}-{nameof(Index)}-{slug}";
        var cacheData = await _cacheService.GetAsync<CategoryViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = await _categoryAppService.GetCategoryAsync(slug!);
        if (cacheData == null) return NotFound();

        await _cacheService.ReplaceAsync(cacheKey, cacheData!, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}