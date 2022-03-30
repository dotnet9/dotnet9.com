using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
using Dotnet9.Web.ViewModels.Albums;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class AlbumController : Controller
{
    private readonly IAlbumAppService _albumAppService;
    private readonly IBlogPostAppService _blogPostAppService;

    public AlbumController(IAlbumAppService albumAppService, IBlogPostAppService blogPostAppService)
    {
        _albumAppService = albumAppService;
        _blogPostAppService = blogPostAppService;
    }

    [Route("album/{slug?}")]
    public async Task<IActionResult> Index(string? slug)
    {
        if (slug.IsNullOrWhiteSpace()) return NotFound();
        var album = await _albumAppService.GetAlbumAsync(slug);
        if (album == null) return NotFound();
        var blogPostList = await _albumAppService.GetBlogPostListAsync(slug);
        if (blogPostList.IsNullOrEmpty()) return NotFound();

        var vm = new AlbumViewModel
        {
            Name = album.Name!,
            Items = blogPostList!
        };
        return View(vm);
    }
}