using Dotnet9.Services.Blogs;
using Dotnet9Tools.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnet9Site.Pages.Post;

public class Post : PageModel
{
    private readonly PostService _postService;

    public Post(PostService postService)
    {
        _postService = postService;
    }

    public PostDetailModel Item { get; set; }

    public async Task<IActionResult> OnGet(int Id)
    {
        Console.WriteLine("Id" + Id);
        PostDetailModel? item = await _postService.Get(Id);
        if (item == null)
        {
            return NotFound();
        }

        Item = item;
        await _postService.Visit(item.Id, HttpContext.GetClientIp(), HttpContext.Request.Headers.UserAgent,
            HttpContext.GetSiteUid());
        return Page();
    }
}