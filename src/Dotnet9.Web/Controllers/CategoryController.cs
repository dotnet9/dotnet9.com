using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

[Authorize]
public partial class CategoryController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly ICacheService _cacheService;
    private readonly ICategoryAppService _categoryAppService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryAppService categoryAppService, ICategoryRepository categoryRepository,
        IBlogPostAppService blogPostAppService,
        ICacheService cacheService, IMapper mapper)
    {
        _categoryAppService = categoryAppService;
        _categoryRepository = categoryRepository;
        _blogPostAppService = blogPostAppService;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("cat/{slug?}")]
    [AllowAnonymous]
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