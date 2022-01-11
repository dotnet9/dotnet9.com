using AutoMapper;
using Dotnet9.Albums;
using Dotnet9.Categories;
using Dotnet9.Tags;

namespace Dotnet9.Blazor;

public class Dotnet9BlazorAutoMapperProfile : Profile
{
    public Dotnet9BlazorAutoMapperProfile()
    {
        CreateMap<TagDto, UpdateTagDto>();
        CreateMap<AlbumDto, UpdateAlbumDto>();
        CreateMap<CategoryDto, UpdateCategoryDto>();
    }
}