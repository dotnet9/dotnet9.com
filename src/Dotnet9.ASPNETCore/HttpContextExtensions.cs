using Microsoft.AspNetCore.Http;

namespace Dotnet9.ASPNETCore;

public static class HttpContextExtensions
{
    public const string XForwardedForKey = "X-Forwarded-For";
    public static string? GetClientIp(this HttpContext context)
    {
        var ip = context.Request.Headers[XForwardedForKey].ToString();
        if (string.IsNullOrEmpty(ip))
        {
            ip = context.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }

        return ip;
    }
}