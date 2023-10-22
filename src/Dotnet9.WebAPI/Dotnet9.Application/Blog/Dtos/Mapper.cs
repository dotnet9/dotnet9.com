namespace Dotnet9.Application.Blog.Dtos;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Categories, TreeSelectOutput>()
            .Map(dest => dest.Label, src => src.Name)
            .Map(dest => dest.Value, src => src.Id);
    }
}