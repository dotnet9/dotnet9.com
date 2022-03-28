using AutoMapper;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Categories;

namespace Dotnet9.Web.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<AlbumCount, AlbumCountDto>();
        CreateMap<Category, CategoryCountDto>();
    }
}