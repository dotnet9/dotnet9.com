using Dotnet9.Models.Dtos.Blogs;
using Dotnet9.Models.Dtos.Blogs.Dto;
using Dotnet9.Services.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnet9Site.Pages.Home;

public class HomeModel : PageModel
{
    private readonly PostService _postService;

    public List<PostItemModel> list = new();


    public HomeModel(PostService postService)
    {
        _postService = postService;
    }

    [BindProperty] public TestUser TestUser { get; set; }
    public string Name { get; set; }

    public int PageIndex { get; set; }

    public int Total { get; set; }

    public async Task OnGet(int pageIndex)
    {
        PageDto<PostItemModel> res = await _postService.GetHomeList(new PostRequestModel
        {
            Index = pageIndex,
            PageSize = 10,
            FilterPublish = true,
            PublishStatus = true
        });
        list.AddRange(res.Data);
        Total = res.Total;
        PageIndex = pageIndex;
        ViewData["title"] = "主页";
    }
}

public class TestUser
{
    public string UserName { get; set; }
}