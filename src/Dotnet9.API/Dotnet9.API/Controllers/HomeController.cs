using AutoMapper;
using Dotnet9.IServices.Base;
using Dotnet9.Models;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.BlogPosts;
using Dotnet9.Models.ViewModels.Questions;
using Dotnet9.Models.ViewModels.UserInfos;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IBaseService<UserInfo> _userInfoService;
    private readonly IBaseService<BlogPost> _blogPostService;
    private readonly IBaseService<Question> _questionService;
    private readonly IBaseService<Advertisement> _advertisementService;
    private readonly IMapper _mapper;

    public HomeController(IBaseService<UserInfo> userInfoService,
        IBaseService<BlogPost> blogPostService,
        IBaseService<Question> questionService,
        IBaseService<Advertisement> advertisementService,
        IMapper mapper)
    {
        _userInfoService = userInfoService;
        _blogPostService = blogPostService;
        _questionService = questionService;
        _advertisementService = advertisementService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<MessageModel<List<BlogPostDto>>> GetBlogPostAsync()
    {
        var entityList = await _blogPostService.GetPagedListAsync(0, 10, nameof(BlogPost.CreateTime));
        var blogPostCreateUserIdList = entityList.Select(x => x.CreateUserId);
        var createUserList = await _userInfoService.GetListAsync(x => blogPostCreateUserIdList.Contains(x.Id));
        var blogPostUpdateUserIdList = entityList.Select(x => x.UpdateUserId);
        var uppateUserList = await _userInfoService.GetListAsync(x => blogPostUpdateUserIdList.Contains(x.Id));
        var response = _mapper.Map<List<BlogPostDto>>(entityList);
        foreach (var item in response)
        {
            var createUser = createUserList.FirstOrDefault(x => x.Id == item.CreateUserId);
            item.CreateUserName = createUser.UserName;
            item.CreateUserPortrait = createUser.HeadPortrait;

            var updateUser = uppateUserList.FirstOrDefault(x => x.Id == item.UpdateUserId);
            item.UpdateUserName = updateUser.UserName;
            item.UpdateUserPortrait = updateUser.HeadPortrait;
        }

        if (response.Count <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                response.Add(new BlogPostDto { Title = $"test{i}" });
            }
        }

        return MessageModel<List<BlogPostDto>>.Success("获取成功", response);
    }

    [HttpGet]
    public async Task<MessageModel<List<QuestionDto>>> GetQuestionAsync()
    {
        var questionList = await _questionService.GetPagedListAsync(0, 10, nameof(Question.CreateTime));

        return MessageModel<List<QuestionDto>>.Success("获取成功", _mapper.Map<List<QuestionDto>>(questionList));
    }

    [HttpGet]
    public async Task<MessageModel<List<UserInfoDto>>> GetUserInfoAsync()
    {
        var userInfoList = await _userInfoService.GetPagedListAsync(0, 5, nameof(UserInfo.CreateTime));

        var response = _mapper.Map<List<UserInfoDto>>(userInfoList);

        // 此处会多次调用数据库操作，实际项目中我们会返回字典来处理
        foreach (var item in response)
        {
            item.QuestionsCount = (int)(await _questionService.GetCountAsync(x => x.CreateUserId == item.Id));
            item.BlogPostCount = (int)(await _blogPostService.GetCountAsync(x => x.CreateUserId == item.Id));
        }

        return MessageModel<List<UserInfoDto>>.Success("获取成功", response);
    }

    [HttpGet]
    public async Task<MessageModel<string>> GetAdvertisement()
    {
        var advertisementList = await _advertisementService.GetPagedListAsync(0, 5, nameof(Advertisement.CreateTime));
        return MessageModel<string>.Success("success");
    }
}