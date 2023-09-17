using Dotnet9.Core;
using Dotnet9.Services.Blogs;
using Dotnet9Tools.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnet9Site.Pages.Post;

public class DetailsByShortIdModel : PageModel
{
    private readonly PostService _postService;

    public DetailsByShortIdModel(PostService postService)
    {
        _postService = postService;
    }

    [BindProperty(SupportsGet = true)] public string? ShortId { get; set; }


    public PostDetailModel? Item { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (!ShortId.IsNullOrWhiteSpace())
        {
            Item = await _postService.GetByShortId(ShortId!);
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