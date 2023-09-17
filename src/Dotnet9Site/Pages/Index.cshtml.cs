using Dotnet9.Models.Dtos.Blogs;
using Dotnet9.Models.Dtos.Blogs.Dto;
using Dotnet9.Services.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnet9Site.Pages;

public class IndexModel : PageModel
{
    private readonly PostService _postService;


    public IndexModel(PostService postService)
    {
        _postService = postService;
    }

    [BindProperty(SupportsGet = true)] public string? Keywords { get; set; }

    [BindProperty(SupportsGet = true)] public int PageIndex { get; set; } = 1;

    [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 12;


    public List<PostItemModel> Posts = new();
    public int Total { get; set; }

    public async Task OnGet()
    {
        PageDto<PostItemModel> res = await _postService.GetHomeList(new PostRequestModel
        {
            Keywords = Keywords,
            PageIndex = PageIndex,
            PageSize = PageSize
        });
        Posts.AddRange(res.Data);
        Total = res.Total;
        ViewData["title"] = "首页";
    }
}