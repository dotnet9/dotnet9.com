using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Core;
using Dotnet9.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class CategoryController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService, IBlogPostAppService blogPostAppService)
    {
        _categoryAppService = categoryAppService;
        _blogPostAppService = blogPostAppService;
    }

    [Route("cat/{slug?}")]
    public async Task<IActionResult> Index(string? slug)
    {
        if (slug.IsNullOrWhiteSpace()) return NotFound();

        var category = await _categoryAppService.GetCategoryAsync(slug!);
        if (category == null) return NotFound();

        var blogPostList = await _categoryAppService.GetBlogPostListAsync(slug!);
        if (blogPostList.IsNullOrEmpty()) return NotFound();

        var vm = new CategoryViewModel
        {
            Name = category.Name!,
            Items = blogPostList!
        };
        return View(vm);
    }
}