namespace Dotnet9.Application.Client.Dtos;

public class TagsOutput
{
    /// <summary>
    /// 标签ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }
    /// <summary>
    /// 标签名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 颜色
    /// </summary>
    public string Color { get; set; }
}