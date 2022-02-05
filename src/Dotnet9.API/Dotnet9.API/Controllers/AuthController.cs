using AutoMapper;
using Dotnet9.Common.Helpers;
using Dotnet9.IServices.Base;
using Dotnet9.Models;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.UserInfos;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBaseService<UserInfo> _userInfoService;

    public AuthController(IBaseService<UserInfo> userInfoService, IMapper mapper)
    {
        _userInfoService = userInfoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<MessageModel<string>> Login(string account, string password)
    {
        var jwtStr = string.Empty;

        if (string.IsNullOrWhiteSpace(account) || string.IsNullOrEmpty(password))
            return MessageModel<string>.Fail("用户名或密码不能为空");

        var md5Password = Md5Helper.Md5Encrypt32(password);
        var userInfo = await _userInfoService.FindAsync(x => x.Account == account && x.Password == md5Password);
        if (userInfo == null) return MessageModel<string>.Fail("认证失败");

        jwtStr = GetUserJwt(userInfo);

        return MessageModel<string>.Success("获取成功", jwtStr);
    }

    [HttpPost]
    public async Task<MessageModel<string>> Register(CreateUserInfoInputDto input)
    {
        var userInfo = await _userInfoService.FindAsync(x => x.Account == input.Account);
        if (userInfo != null) return MessageModel<string>.Fail("账号已存在");

        userInfo = await _userInfoService.FindAsync(x => x.Email == input.Email);
        if (userInfo != null) return MessageModel<string>.Fail("邮箱已存在");

        userInfo = await _userInfoService.FindAsync(x => x.Phone == input.Phone);
        if (userInfo != null) return MessageModel<string>.Fail("手机号已存在");

        userInfo = await _userInfoService.FindAsync(x => x.UserName == input.UserName);
        if (userInfo != null) return MessageModel<string>.Fail("用户名已存在");

        input.Password = Md5Helper.Md5Encrypt32(input.Password);

        var user = _mapper.Map<UserInfo>(input);
        user.CreateTime = DateTime.UtcNow;
        await _userInfoService.InsertAsync(user, true);
        var jwtStr = GetUserJwt(user);

        return MessageModel<string>.Success("注册成功", jwtStr);
    }

    private static string GetUserJwt(UserInfo userInfo)
    {
        var tokenModel = new TokenModelJwt { Uid = userInfo.Id, Role = "User" };
        var jwtStr = JwtHelper.IssueJwt(tokenModel);
        return jwtStr;
    }
}