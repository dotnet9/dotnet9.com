using AutoMapper;
using Dotnet9.Application.Contracts.Abouts;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Application.Contracts.Donations;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Domain.Abouts;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Donations;
using Dotnet9.Domain.UrlLinks;

namespace Dotnet9.Web.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<AlbumCount, AlbumCountDto>();
        CreateMap<Album, AlbumDto>();
        CreateMap<Category, CategoryCountDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<BlogPostWithDetails, BlogPostWithDetailsDto>();
        CreateMap<UrlLink, UrlLinkDto>();
        CreateMap<About, AboutDto>().ReverseMap();
        CreateMap<Donation, DonationDto>().ReverseMap();
    }
}