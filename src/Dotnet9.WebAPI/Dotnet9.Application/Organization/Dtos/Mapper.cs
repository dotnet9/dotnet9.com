namespace Dotnet9.Application.Organization.Dtos;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //机构映射为下拉选项
        config.ForType<SysOrganization, TreeSelectOutput>()
            .Map(dest => dest.Value, src => src.Id)
            .Map(dest => dest.Label, src => src.Name);
    }
}