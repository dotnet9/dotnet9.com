namespace Dotnet9.Web.Utils;

public static class HttpContextExtensions
{
    public static string? GetClientIP(this Microsoft.AspNetCore.Http.HttpContext context)
    {
        var ip = context.Request.Headers["X-Forwarded-For"].ToString();
        if (string.IsNullOrEmpty(ip)) ip = context.Connection.RemoteIpAddress?.ToString();
        return ip;
    }
}