using AutoMapper;
using Dotnet9.Common.Helpers;
using Dotnet9.IServices;
using Dotnet9.IServices.Base;
using Dotnet9.Models;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.UserInfos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class UserInfoController : ControllerBase
{
    private readonly IBaseService<UserInfo> _userInfoService;
    private readonly IBlogPostService _blogPostService;
    private readonly IBaseService<Question> _questionService;
    private readonly IMapper _mapper;

    public UserInfoController(IBaseService<UserInfo> userInfoService, IBlogPostService blogPostService,
        IBaseService<Question> questionService, IMapper mapper)
    {
        _userInfoService = userInfoService;
        _blogPostService = blogPostService;
        _questionService = questionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<MessageModel<UserInfoDetailsDto>> GetAsync()
    {
        var token = JwtHelper.ParsingJwtToken(HttpContext);
        var userInfo = await _userInfoService.FindAsync(x => x.Id == token.Uid);

        return MessageModel<UserInfoDetailsDto>.Success("获取成功", _mapper.Map<UserInfoDetailsDto>(userInfo));
    }

    [HttpPut]
    public async Task<MessageModel<string>> UpdateAsync(UpdateUserInfoInputDto input)
    {
        var token = JwtHelper.ParsingJwtToken(HttpContext);
        var userInfo = await _userInfoService.FindAsync(x => x.Id == token.Uid);

        userInfo = _mapper.Map<UserInfo>(input);
        await _userInfoService.UpdateAsync(userInfo, true);

        return MessageModel<string>.Success("修改成功");
    }

    [HttpGet]
    public async Task<MessageModel<UserInfoDto>> GetAuthorAsync(int id)
    {
        var entity = await _blogPostService.FindAsync(x => x.Id == id);
        var user = await _userInfoService.FindAsync(x => x.Id == entity.CreateUserId);
        var response = _mapper.Map<UserInfoDto>(user);
        response.BlogPostCount = (int)(await _blogPostService.GetCountAsync(x => x.CreateUserId == user.Id));
        response.QuestionsCount =
            (int)(await _questionService.GetCountAsync(x => x.CreateUserId == user.Id));
        return MessageModel<UserInfoDto>.Success("获取成功", response);
    }
}