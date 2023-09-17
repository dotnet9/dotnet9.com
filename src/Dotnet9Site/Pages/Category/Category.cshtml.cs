using Dotnet9.Models.Dtos.Blogs;
using Dotnet9.Models.Dtos.Blogs.Dto;
using Dotnet9.Services.Blogs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnet9Site.Pages.Category;

public class Category : PageModel
{
    private readonly PostCateService _postCateService;


    private readonly PostService _postService;

    public PageDto<PostItemModel> pageDto;

    public Category(PostService postService, PostCateService postCateService)
    {
        _postService = postService;
        _postCateService = postCateService;
    }

    public int PageIndex { get; set; }
    public Guid CateId { get; set; }

    public int PageSize { get; set; } = 10;

    public string? CateName { get; set; }

    public async Task OnGet(Guid cateid)
    {
        PageIndex = 1;
        CateId = cateid;
        pageDto = await _postService.GetHomeList(new PostRequestModel
        {
            CateId = CateId,
            PageIndex = PageIndex,
            PageSize = PageSize,
            FilterPublish = true,
            PublishStatus = true
        });
        CateName = await _postCateService.GetCateNameById(cateid);
    }
}