using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.Timelines;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Domain.Users;

namespace Dotnet9.Web.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<AlbumCount, AlbumCountDto>();
        CreateMap<Album, AlbumDto>();
        CreateMap<AlbumBrief, AlbumBriefDto>();
        CreateMap<AddOrUpdateAlbumDto, Album>();

        CreateMap<Category, CategoryCountDto>();
        CreateMap<CategoryCount, CategoryCountDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryBrief, CategoryBriefDto>();
        CreateMap<AddOrUpdateCategoryDto, Category>();

        CreateMap<TagCount, TagCountDto>();

        CreateMap<BlogPostWithDetails, BlogPostWithDetailsDto>();
        CreateMap<BlogPost, BlogPostForSitemap>();
        CreateMap<BlogPostBrief, BlogPostBriefDto>();
        CreateMap<BlogPost, BlogPostDto>();

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
        CreateMap<LatestActionLog, LatestActionLogDto>();

        CreateMap<AddOrUpdateUrlLinkDto, UrlLink>();
    }
}