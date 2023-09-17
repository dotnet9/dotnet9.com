using Dotnet9.Core;
using Dotnet9.Services.Blogs;
using Dotnet9Tools.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnet9Site.Pages.Post;

public class DetailsBySlugModel : PageModel
{
    private readonly PostService _postService;

    public DetailsBySlugModel(PostService postService)
    {
        _postService = postService;
    }

    [BindProperty(SupportsGet = true)] public string? Year { get; set; }
    [BindProperty(SupportsGet = true)] public string? Month { get; set; }
    [BindProperty(SupportsGet = true)] public string? Slug { get; set; }


    public PostDetailModel? Item { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (!Slug.IsNullOrWhiteSpace())
        {
            Item = await _postService.GetBySlug(Slug!);
        }

        if (Item == null)
        {
            return NotFound();
        }

        await _postService.Visit(Item.Id, HttpContext.GetClientIp(), HttpContext.Request.Headers.UserAgent,
            HttpContext.GetSiteUid());
        return Page();
    }
}