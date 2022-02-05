using AutoMapper;
using Dotnet9.Models.Models;
using Dotnet9.Models.ViewModels.UserInfos;

namespace Dotnet9.Extensions.AutoMapper;

public class UserInfoProfile : Profile
{
    public UserInfoProfile()
    {
        CreateMap<CreateUserInfoInputDto, UserInfo>();
        CreateMap<UserInfo, UserInfoDto>();

        CreateMap<UserInfo, UserInfoDetailsDto>();
    }
}