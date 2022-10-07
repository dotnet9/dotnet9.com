using Dotnet9.Application.Contracts;
using Dotnet9.Application.Contracts.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public partial class BlogPostController
{
    [HttpGet("api/post/list")]
    public async Task<PageDto<BlogPostDto>> List([FromQuery] BlogPostRequest request)
    {
        return await _blogPostAppService.AdminListAsync(request);
    }

    [HttpGet("api/post/get")]
    public async Task<BlogPostWithDetailsDto?> Get(int id)
    {
        return await _blogPostAppService.GetByIdAsync(id);
    }

    [HttpDelete("api/post/delete")]
    public async Task Delete(int id)
    {
        await _blogPostRepository.DeleteAsync(x => x.Id == id);
    }
}