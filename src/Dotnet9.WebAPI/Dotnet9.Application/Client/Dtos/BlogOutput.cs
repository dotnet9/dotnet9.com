namespace Dotnet9.Application.Client.Dtos;

public class BlogOutput
{
    /// <summary>
    /// 博客基本设置信息
    /// </summary>
    public BlogSetting? Site { get; set; }
    /// <summary>
    /// 博主信息
    /// </summary>
    public BloggerInfo? Info { get; set; }
    /// <summary>
    /// 各个页面封面图
    /// </summary>
    public Dictionary<string, List<string>>? Covers { get; set; }
}