namespace Dotnet9Tools.Configs.Models;

/// <summary>
/// 网站基本配置信息
/// </summary>
public class SiteOptions
{
    /// <summary>
    /// 网站创建年份
    /// </summary>
    public int Start { get; set; }

    /// <summary>
    /// 种子数据根路径绝对路径
    /// </summary>
    public string AssetsLocalPath { get; set; } = null!;
}