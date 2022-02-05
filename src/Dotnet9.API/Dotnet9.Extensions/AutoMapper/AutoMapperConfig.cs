using AutoMapper;

namespace Dotnet9.Extensions.AutoMapper;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserInfoProfile());
            cfg.AddProfile(new BlogPostProfile());
            cfg.AddProfile(new QuestionProfile());
        });
    }
}