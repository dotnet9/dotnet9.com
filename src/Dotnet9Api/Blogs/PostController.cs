using Dotnet9.Models.Dtos.Blogs;
using Dotnet9.Models.Dtos.Blogs.Dto;
using Dotnet9.Services.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9Api.Blogs;

/// <summary>
///     文章
/// </summary>
public class PostController : BaseAdminController
{
    private readonly PostService _post;


    public PostController(PostService post)
    {
        _post = post;
    }

    [HttpGet]
    public async Task<PostDetailModel> Get(int id)
    {
        return await _post.Get(id);
    }

    /// <summary>
    ///     置顶
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task Top(BaseIdModel<int> model)
    {
        await _post.Top(model.Id);
    }

    /// <summary>
    ///     文章列表
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageDto<PostItemModel>> List([FromQuery] PostRequestModel model)
    {
        PageDto<PostItemModel> res = await _post.GetHomeList(model);
        return res;
    }


    /// <summary>
    ///     新增和编辑文章
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task Edit(PostEditRequest request)
    {
        await _post.Edit(request);
    }

    /// <summary>
    ///     发布/隐藏
    /// </summary>
    /// <param name="model"></param>
    [HttpPost]
    public async Task Publish(BaseIdModel<int> model)
    {
        await _post.Publish(model.Id);
    }
}