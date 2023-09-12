using Microsoft.AspNetCore.Http;

namespace Dotnet9Tools.Helper;

public static class HttpExtensions
{
    /// <summary>
    ///     获取IP地址
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetClientIp(this HttpContext context)
    {
        string? ip;
        ip = context.Request.Headers.Where(a => a.Key == "Cdn-Src-Ip").Select(a => a.Value.ToString())
            .FirstOrDefault();
        if (!string.IsNullOrEmpty(ip))
        {
            return IpReplace(ip);
        }

        ip = context.Request.Headers.Where(a => a.Key == "X-Forwarded-For").Select(a => a.Value.ToString())
            .FirstOrDefault();
        if (!string.IsNullOrEmpty(ip))
        {
            return IpReplace(ip);
        }

        if (string.IsNullOrWhiteSpace(ip))
        {
            ip = context.Connection.RemoteIpAddress?.ToString();
        }

        if (string.IsNullOrWhiteSpace(ip))
        {
            return "未知";
        }

        return IpReplace(ip);
    }

    private static string IpReplace(string inip)
    {
        //::ffff:
        //::ffff:192.168.2.131 这种IP处理
        if (inip.Contains("::ffff:"))
        {
            inip = inip.Replace("::ffff:", "");
        }

        return inip;
    }
}