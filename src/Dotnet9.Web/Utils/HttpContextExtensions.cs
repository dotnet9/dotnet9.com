namespace Dotnet9.Web.Utils;

public static class HttpContextExtensions

{
    public static ContextInfo GetRequestInfo(this HttpContext context)
    {
        return new ContextInfo
        {
            Origin = context.Request.Headers["Origin"].FirstOrDefault(),
            IP = context.GetClientIP()
        };
    }

    public static string? GetClientIP(this HttpContext context)
    {
        var ip = context.Request.Headers["X-Forwarded-For"].ToString();
        if (string.IsNullOrEmpty(ip)) ip = context.Connection.RemoteIpAddress?.ToString();
        return ip;
    }
}

public class ContextInfo
{
    public string? Origin { get; set; }
    public string? IP { get; set; }
}