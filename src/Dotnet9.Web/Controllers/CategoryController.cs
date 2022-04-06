using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Core;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

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

    [Route("cat/{slug?}")]
    public async Task<IActionResult> Index(string? slug)
    {
        if (slug.IsNullOrWhiteSpace()) return NotFound();


        var cacheKey = $"{nameof(CategoryController)}-{nameof(Index)}-{slug}";
        var cacheData = await _cacheService.GetAsync<CategoryViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        var category = await _categoryAppService.GetCategoryAsync(slug!);
        if (category == null) return NotFound();

        var blogPostList = await _categoryAppService.GetBlogPostListAsync(slug!);
        if (blogPostList.IsNullOrEmpty()) return NotFound();

        cacheData = new CategoryViewModel
        {
            Name = category.Name!,
            Items = blogPostList!
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}