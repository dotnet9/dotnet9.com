namespace Dotnet9.Application.Menu.Dtos;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SysMenu, TreeSelectOutput>()
            .Map(dest => dest.Label, src => src.Name)
            .Map(dest => dest.Value, src => src.Id);

        config.ForType<SysMenu, RouterOutput>()
            .Map(dest => dest.Name, src => src.RouteName)
            .Map(dest => dest.Component, src => src.Component)
            .Map(dest => dest.Path, src => src.Path)
            .Map(dest => dest.Meta, src => new RouterMetaOutput()
            {
                Type = src.Type,
                IsKeepAlive = src.IsKeepAlive,
                Icon = src.Icon,
                IsAffix = src.IsFixed,
                IsHide = !src.IsVisible,
                IsLink = src.Link,
                Title = src.Name
            });
    }
}