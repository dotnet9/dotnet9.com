namespace Dotnet9.Application.Config.Dtos;

public class CustomConfigDetailOutput
{
    /// <summary>
    /// 表单渲染Json
    /// </summary>
    public JObject FormJson { get; set; }

    /// <summary>
    /// 表单数据
    /// </summary>
    public JObject DataJson { get; set; }

    /// <summary>
    /// 配置项Id
    /// </summary>
    public long ItemId { get; set; }
}