using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
using Dotnet9.Web.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class BlogPostController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;

    public BlogPostController(IBlogPostAppService blogPostAppService)
    {
        _blogPostAppService = blogPostAppService;
    }

    [Route("{year}/{month}/{slug?}")]
    public async Task<IActionResult> Index(int year, int month, string? slug)
    {
        if (slug.IsNullOrWhiteSpace()) return NotFound();

        var blogPostWithDetailsDto = await _blogPostAppService.FindBySlugAsync(slug!);
        if (blogPostWithDetailsDto == null) return NotFound();

        var vm = new BlogPostViewModel
        {
            BlogPost = blogPostWithDetailsDto
        };
        return View(vm);
    }
}