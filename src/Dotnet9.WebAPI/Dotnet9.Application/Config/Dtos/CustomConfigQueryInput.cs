namespace Dotnet9.Application.Config.Dtos;

public class CustomConfigQueryInput : Pagination
{
    /// <summary>
    /// 配置名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 配置唯一编码
    /// </summary>
    public string Code { get; set; }
}