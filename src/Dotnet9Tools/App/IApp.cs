using Dotnet9Tools.Helper;
using Microsoft.AspNetCore.Http;

namespace Dotnet9Tools.App;

public interface IApp
{
    string GetClientIp();

    string GetUserAgent();
}

public class WebApp : IApp
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WebApp(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetClientIp()
    {
        string? ip = _httpContextAccessor.HttpContext?.GetClientIp();
        return ip ?? "未知";
    }

    public string GetUserAgent()
    {
        string? userAgent = _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString();
        return string.IsNullOrWhiteSpace(userAgent) ? "" : userAgent;
    }
}