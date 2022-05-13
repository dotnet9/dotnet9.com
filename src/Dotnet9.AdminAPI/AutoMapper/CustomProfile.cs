using System.Net;
using AutoMapper;
using Dotnet9.AdminAPI.ViewModels.Accounts;
using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Core;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.UrlLinks;
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
                    src => DateTimeOffsetHelper.DateTimeToString(src.CreateDate)));
        CreateMap<ActionLog, LatestActionLogItemDto>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => WebUtility.UrlDecode(src.Url)))
            .ForMember(dest => dest.CreateDate,
                opt => opt.MapFrom(
                    src => DateTimeOffsetHelper.DateTimeToString(src.CreateDate)));
        CreateMap<Top10AccessPageItem, Top10AccessPageItemDto>();
        CreateMap<Top10AccessPage, Top10AccessPageDto>();
        CreateMap<Top10SearchItem, Top10SearchItemDto>();
        CreateMap<Top10Search, Top10SearchDto>();
        CreateMap<LatestActionLog, LatestActionLogDto>()
            .ForMember(dest => dest.LatestDate,
                opt => opt.MapFrom(src => DateTimeOffsetHelper.DateTimeToString(src.LatestDate)));

        CreateMap<AddOrUpdateUrlLinkDto, UrlLink>();
    }
}