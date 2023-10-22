using Microsoft.Extensions.Logging;

namespace Dotnet9.Application.Logging.Dtos;

public class LogPageQueryInput : Pagination
{
    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; }

    /// <summary>
    /// 日志级别
    /// </summary>
    public LogLevel? LogLevel { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Account { get; set; }
}