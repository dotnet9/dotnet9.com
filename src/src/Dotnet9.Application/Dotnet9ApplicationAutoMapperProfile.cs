using AutoMapper;
using Dotnet9.Tags;

namespace Dotnet9
{
    public class Dotnet9ApplicationAutoMapperProfile : Profile
    {
        public Dotnet9ApplicationAutoMapperProfile()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<CreateTagDto, Tag>();
        }
    }
}
