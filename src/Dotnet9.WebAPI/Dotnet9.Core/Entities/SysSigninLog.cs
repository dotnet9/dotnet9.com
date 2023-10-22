namespace Dotnet9.Core.Entities;
/// <summary>
/// 用户登录日志
/// </summary>
public class SysSigninLog : Entity<long>, ICreatedTime
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// ip地址
    /// </summary>
    [SugarColumn(Length = 64)]
    public string RemoteIp { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [SugarColumn(Length = 256)]
    public string UserAgent { get; set; }

    /// <summary>
    /// Ip归属地
    /// </summary>
    [SugarColumn(Length = 128)]
    public string Location { get; set; }

    /// <summary>
    /// 用户系统信息
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? OsDescription { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Message { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}