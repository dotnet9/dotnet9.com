using Dotnet9Api.Blogs.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9Api.Blogs;

public class PostTagController : BaseAdminController
{
    private readonly PostTagService _postTagService;

    public PostTagController(PostTagService postTagService)
    {
        _postTagService = postTagService;
    }


    /// <summary>
    ///     获取标签列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageDto<TagDtoModel>> GetList([FromQuery] TagListRequest request)
    {
        return await _postTagService.GetList(request);
    }
}