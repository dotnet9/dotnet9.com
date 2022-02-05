using AutoMapper;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.Questions;

namespace Dotnet9.Extensions.AutoMapper;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<CreateQuestionInputDto, Question>();
        CreateMap<UpdateQuestionInputDto, Question>();

        CreateMap<Question, QuestionDto>()
            .ForMember(left => left.CommentCount, right => right.MapFrom(x => x.QuestionComments.Count));

        CreateMap<Question, QuestionDetailsDto>();

        CreateMap<QuestionComment, QuestionCommentDto>()
            .ForMember(left => left.CreateUserName, right => right.MapFrom(x => x.CreateUser.UserName))
            .ForMember(left => left.CreateUserPortrait, right => right.MapFrom(x => x.CreateUser.HeadPortrait))
            .ForMember(left => left.UpdateUserName, right => right.MapFrom(x => x.UpdateUser.UserName))
            .ForMember(left => left.UpdateUserPortrait, right => right.MapFrom(x => x.UpdateUser.HeadPortrait));

        CreateMap<CreateQuestionCommentInputDto, QuestionComment>();
    }
}