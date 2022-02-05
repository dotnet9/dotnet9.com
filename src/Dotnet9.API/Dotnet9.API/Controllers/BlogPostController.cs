using AutoMapper;
using Dotnet9.Common.Helpers;
using Dotnet9.IServices;
using Dotnet9.IServices.Base;
using Dotnet9.Models;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.BlogPosts;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BlogPostController : ControllerBase
{
    private readonly IBlogPostService _blogPostService;
    private readonly IBaseService<UserInfo> _userInfoService;
    private readonly IMapper _mapper;

    public BlogPostController(IBlogPostService blogPostService, IBaseService<UserInfo> userInfoService, IMapper mapper)
    {
        _blogPostService = blogPostService;
        _userInfoService = userInfoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<MessageModel<List<BlogPostDto>>> GetListAsync(int page, int pageSize)
    {
        var entityList = await _blogPostService.GetPagedListAsync(page, pageSize, nameof(BlogPost.CreateTime));
        var blogPostCreateUserIdList = entityList.Select(x => x.CreateUserId);
        var createUserList = await _userInfoService.GetListAsync(x => blogPostCreateUserIdList.Contains(x.Id));
        var blogPostUpdateUserIdList = entityList.Select(x => x.UpdateUserId);
        var updateUserList = await _userInfoService.GetListAsync(x => blogPostUpdateUserIdList.Contains(x.Id));
        var response = _mapper.Map<List<BlogPostDto>>(entityList);
        foreach (var item in response)
        {
            var createUser = createUserList.FirstOrDefault(x => x.Id == item.CreateUserId);
            item.CreateUserName = createUser?.UserName;
            item.CreateUserPortrait = createUser?.HeadPortrait;

            var updateUser = updateUserList.FirstOrDefault(x => x.Id == item.CreateUserId);
            item.UpdateUserName = updateUser?.UserName;
            item.UpdateUserPortrait = updateUser?.HeadPortrait;
        }

        return MessageModel<List<BlogPostDto>>.Success("获取成功", response);
    }

    [HttpGet]
    public async Task<MessageModel<BlogPostDetailsDto>> GetAsync(int id)
    {
        var entity = await _blogPostService.GetBlogPostDetailsAsync(id);
        var result = _mapper.Map<BlogPostDetailsDto>(entity);
        return MessageModel<BlogPostDetailsDto>.Success("获取成功", result);
    }

    [HttpPost]
    public async Task<MessageModel<string>> CreateAsync(CreateBlogPostInputDto input)
    {
        var token = JwtHelper.ParsingJwtToken(HttpContext);

        var entity = _mapper.Map<BlogPost>(input);
        entity.CreateTime = DateTime.UtcNow;
        entity.CreateUserId = (int)token.Uid;
        await _blogPostService.InsertAsync(entity, true);

        return MessageModel<string>.Success("创建成功");
    }

    [HttpPut]
    public async Task<MessageModel<string>> UpdateAsync(int id, UpdateBlogPostInputDto input)
    {
        var entity = await _blogPostService.FindAsync(d => d.Id == id);

        entity = _mapper.Map(input, entity);

        await _blogPostService.UpdateAsync(entity!, true);
        return MessageModel<string>.Success("修改成功");
    }

    [HttpDelete]
    public async Task<MessageModel<string>> DeleteAsync(int id)
    {
        var entity = await _blogPostService.FindAsync(d => d.Id == id);
        await _blogPostService.DeleteAsync(entity, true);
        return MessageModel<string>.Success("删除成功");
    }

    [HttpPost]
    public async Task<MessageModel<string>> CreateUserInfoBlogPostAsync(int id)
    {
        var token = JwtHelper.ParsingJwtToken(HttpContext);
        await _blogPostService.AddUserInfoBlogPostAsync(id, (int)token.Uid);
        return MessageModel<string>.Success("收藏成功");
    }

    [HttpPost]
    public async Task<MessageModel<string>> CreateBlogPostCommentAsync(int id, CreateBlogPostCommentInputDto input)
    {
        var token = JwtHelper.ParsingJwtToken(HttpContext);
        await _blogPostService.AddBlogPostComment(id, (int)token.Uid, input.Content);
        return MessageModel<string>.Success("评论成功");
    }

    [HttpDelete]
    public async Task<MessageModel<string>> DeleteBlogPostCommentAsync(int blogPostId, int id)
    {
        var entity = await _blogPostService.GetByIdAsync(blogPostId);
        entity.BlogPostComments.Remove(entity.BlogPostComments.FirstOrDefault(x => x.Id == id));
        await _blogPostService.UpdateAsync(entity, true);
        return MessageModel<string>.Success("删除评论成功");
    }
}