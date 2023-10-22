using Microsoft.Extensions.Logging;

namespace Dotnet9.Application.Logging.Dtos;

public class OperationLogPageOutput
{
    /// <summary>
    /// 日志ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 日志描述
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 日志级别
    /// </summary>
    public LogLevel LogLevel { get; set; }
    /// <summary>
    /// 日志详细信息
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// 路由名称
    /// </summary>
    public string ActionName { get; set; }
    /// <summary>
    /// 控制器名称
    /// </summary>
    public string ControllerName { get; set; }
    /// <summary>
    /// 请求方式
    /// </summary>
    public string HttpMethod { get; set; }
    /// <summary>
    /// 状态码
    /// </summary>
    public int HttpStatusCode { get; set; }
    /// <summary>
    /// 耗时（毫秒）
    /// </summary>
    public int Elapsed { get; set; }
    /// <summary>
    /// 异常信息
    /// </summary>
    public string Exception { get; set; }
    /// <summary>
    /// 地理位置
    /// </summary>
    public string Location { get; set; }
    /// <summary>
    /// ip地址
    /// </summary>
    public string RemoteIp { get; set; }
    /// <summary>
    /// 客户端系统信息
    /// </summary>
    public string OsDescription { get; set; }
    /// <summary>
    /// 浏览器
    /// </summary>
    public string UserAgent { get; set; }
    /// <summary>
    /// 请求参数
    /// </summary>
    public string Parameter { get; set; }
    /// <summary>
    /// 响应参数
    /// </summary>
    public string Response { get; set; }
    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; set; }
    /// <summary>
    /// 操作人
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}