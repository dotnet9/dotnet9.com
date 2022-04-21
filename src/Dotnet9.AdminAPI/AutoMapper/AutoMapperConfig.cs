using AutoMapper;

namespace Dotnet9.AdminAPI.AutoMapper;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg => { cfg.AddProfile(new CustomProfile()); });
    }
}