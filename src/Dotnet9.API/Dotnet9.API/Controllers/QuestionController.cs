using AutoMapper;
using Dotnet9.Common.Helpers;
using Dotnet9.IServices;
using Dotnet9.IServices.Base;
using Dotnet9.Models;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;
    private readonly IBaseService<UserInfo> _userInfoService;
    private readonly IMapper _mapper;

    public QuestionController(IQuestionService questionService, IBaseService<UserInfo> userInfoService, IMapper mapper)
    {
        _questionService = questionService;
        _userInfoService = userInfoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<MessageModel<List<QuestionDto>>> GetListAsync(int page, int pageSize)
    {
        var entityList = await _questionService.GetPagedListAsync(page, pageSize, nameof(Question.CreateTime));

        return MessageModel<List<QuestionDto>>.Success("获取成功", _mapper.Map<List<QuestionDto>>(entityList));
    }

    [HttpGet]
    public async Task<MessageModel<QuestionDetailsDto>> GetAsync(int id)
    {
        var entity = await _questionService.GetQuestionDetailsAsync(id);
        var result = _mapper.Map<QuestionDetailsDto>(entity);
        return MessageModel<QuestionDetailsDto>.Success("获取成功", result);
    }

    [HttpPost]
    public async Task<MessageModel<string>> CreateAsync(CreateQuestionInputDto input)
    {
        var token = JwtHelper.ParsingJwtToken(HttpContext);

        var entity = _mapper.Map<Question>(input);
        entity.Traffic = 1;
        entity.CreateTime = DateTime.Now;
        entity.CreateUserId = (int)token.Uid;
        await _questionService.InsertAsync(entity, true);

        return MessageModel<string>.Success("创建成功");
    }

    [HttpPut]
    public async Task<MessageModel<string>> UpdateAsync(int id, UpdateQuestionInputDto input)
    {
        var entity = await _questionService.FindAsync(d => d.Id == id);

        entity = _mapper.Map(input, entity);

        await _questionService.UpdateAsync(entity, true);
        return MessageModel<string>.Success("修改成功");
    }

    [HttpDelete]
    public async Task<MessageModel<string>> DeleteAsync(int id)
    {
        var entity = await _questionService.FindAsync(d => d.Id == id);
        await _questionService.DeleteAsync(entity, true);
        return MessageModel<string>.Success("删除成功");
    }

    [HttpPost]
    public async Task<MessageModel<string>> CreateQuestionCommentsAsync(int id, CreateQuestionCommentInputDto input)
    {
        var token = JwtHelper.ParsingJwtToken(HttpContext);
        await _questionService.AddQuestionComment(id, (int)token.Uid, input.Content);
        return MessageModel<string>.Success("评论成功");
    }

    [HttpDelete]
    public async Task<MessageModel<string>> DeleteQuestionCommentsAsync(int questionId, int id)
    {
        var entity = await _questionService.GetByIdAsync(questionId);
        entity.QuestionComments!.Remove(entity.QuestionComments!.FirstOrDefault(x => x.Id == id));
        await _questionService.UpdateAsync(entity, true);
        return MessageModel<string>.Success("删除评论成功");
    }
}