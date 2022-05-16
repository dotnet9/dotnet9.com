using AutoMapper;
using Dotnet9.Application.Contracts.Abouts;
using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Application.Contracts.Donations;
using Dotnet9.Application.Contracts.Privacies;
using Dotnet9.Application.Contracts.Tags;
using Dotnet9.Application.Contracts.Timelines;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Core;
using Dotnet9.Domain.Abouts;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Donations;
using Dotnet9.Domain.Privacies;
using Dotnet9.Domain.Tags;
using Dotnet9.Domain.Timelines;
using Dotnet9.Domain.UrlLinks;
using Dotnet9.Domain.Users;
using System.Net;
using Dotnet9.Web.ViewModels.Accounts;

namespace Dotnet9.Web.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<AlbumCount, AlbumCountDto>();
        CreateMap<Album, AlbumDto>();

        CreateMap<AlbumBrief, AlbumBriefDto>();

        CreateMap<Category, CategoryCountDto>();
        CreateMap<CategoryCount, CategoryCountDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryBrief, CategoryBriefDto>();

        CreateMap<TagCount, TagCountDto>();

        CreateMap<BlogPostWithDetails, BlogPostWithDetailsDto>();
        CreateMap<BlogPost, BlogPostForSitemap>();
        CreateMap<BlogPostBrief, BlogPostBriefDto>();

        CreateMap<UrlLink, UrlLinkDto>()
            .ForMember(dest => dest.CreateDate,
                opt => opt.MapFrom(src => DateTimeOffsetHelper.DateTimeToString(src.CreateDate)));
        ;

        CreateMap<About, AboutDto>().ReverseMap();

        CreateMap<Donation, DonationDto>().ReverseMap();

        CreateMap<Timeline, TimelineDto>();

        CreateMap<Privacy, PrivacyDto>().ReverseMap();
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