namespace Dotnet9.Application.Logging.Dtos;

public class SigninLogPageOutput
{
    /// <summary>
    /// 日志ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// ip地址
    /// </summary>
    public string RemoteIp { get; set; }
    /// <summary>
    /// 地理位置
    /// </summary>
    public string Location { get; set; }
    /// <summary>
    /// 客户端系统信息
    /// </summary>
    public string OsDescription { get; set; }
    /// <summary>
    /// 客户端浏览器信息
    /// </summary>
    public string UserAgent { get; set; }
    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
    /// <summary>
    /// 登录人账户名
    /// </summary>
    public string Account { get; set; }
}