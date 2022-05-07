using System.Net;
using AutoMapper;
using Dotnet9.AdminAPI.ViewModels.Accounts;
using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.Users;

namespace Dotnet9.AdminAPI.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<AdminAccountForCreationViewModel, UserForCreationDto>();
        CreateMap<UserForCreationDto, AdminAccountForCreation>();
        CreateMap<AccountLoginViewModel, UserForLoginDto>();
        CreateMap<ActionLog, ActionLogDto>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => WebUtility.UrlDecode(src.Url)))
            .ForMember(dest => dest.CreateDate,
                opt => opt.MapFrom(
                    src => DateTimeToString(src.CreateDate)));
    }

    public string? DateTimeToString(DateTimeOffset? date)
    {
        return date?.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
    }
}