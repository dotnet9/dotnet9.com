namespace Dotnet9.Core.Options;

/// <summary>
/// 数据库选项配置
/// </summary>
public sealed class DbConnectionOptions : IConfigurableOptions
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public List<ConnectionConfigExt> Connections { get; set; }
}

/// <summary>
/// 数据库连接扩展
/// </summary>
public class ConnectionConfigExt : ConnectionConfig
{
    /// <summary>
    /// 是否初始化数据库
    /// </summary>
    public bool EnableInitDb { get; set; }

    /// <summary>
    /// 启用表数据初始化
    /// </summary>
    public bool EnableSeed { get; set; }
}