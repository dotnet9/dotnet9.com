using AutoMapper;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.BlogPosts;

namespace Dotnet9.Extensions.AutoMapper;

public class BlogPostProfile : Profile
{
    public BlogPostProfile()
    {
        CreateMap<CreateBlogPostInputDto, BlogPost>();
        CreateMap<UpdateBlogPostInputDto, BlogPost>();

        CreateMap<BlogPost, BlogPostDto>();
        CreateMap<BlogPost, BlogPostDetailsDto>();

        CreateMap<BlogPostComment, BlogPostCommentDto>()
            .ForMember(left => left.CreateUserName, right => right.MapFrom(x => x.CreateUser.UserName))
            .ForMember(left => left.CreateUserPortrait, right => right.MapFrom(x => x.UpdateUser.HeadPortrait))
            .ForMember(left => left.UpdateUserName, right => right.MapFrom(x => x.UpdateUser.UserName))
            .ForMember(left => left.UpdateUserPortrait, right => right.MapFrom(x => x.UpdateUser.HeadPortrait));
    }
}