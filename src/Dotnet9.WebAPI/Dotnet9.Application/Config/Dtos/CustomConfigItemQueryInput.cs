namespace Dotnet9.Application.Config.Dtos;

public class CustomConfigItemQueryInput : Pagination
{
    /// <summary>
    /// 配置ID
    /// </summary>
    public long Id { get; set; }
}