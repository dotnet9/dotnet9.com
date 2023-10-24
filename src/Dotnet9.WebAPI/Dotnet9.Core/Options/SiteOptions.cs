namespace Dotnet9.Core.Options;

/// <summary>
/// 网站基本信息配置
/// </summary>
public sealed class SiteOptions : IConfigurableOptions
{
    /// <summary>
    /// 网站资源目录，需要从https://github.com/dotnet9/Assets.Dotnet9克隆后的本地绝对路径
    /// </summary>
    public string Assets { get; set; }
}