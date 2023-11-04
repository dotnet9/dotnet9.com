namespace Dotnet9.Core.Options;

/// <summary>
/// 网站基本信息配置
/// </summary>
public sealed class SiteOptions : IConfigurableOptions
{
    /// <summary>
    /// 网站地址，比如：https://dotnet9.com
    /// </summary>
    public string Domain { get; set; } = null!;

    /// <summary>
    /// 网站资源目录，需要从https://github.com/dotnet9/Assets.Dotnet9克隆后的本地绝对路径
    /// </summary>
    public string AssetsDir { get; set; } = null!;

    /// <summary>
    /// 网站资源Url，用于拼接资源的全路径，比如：https://img1.dotnet9.com/
    /// </summary>
    public string AssetsUrl { get; set; } = null!;

    /// <summary>
    /// 网站所有人
    /// </summary>
    public string Owner { get; set; } = null!;

    /// <summary>
    /// 网站创建年份，比如：2019
    /// </summary>
    public int Start { get; set; }
}