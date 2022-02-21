using Dotnet9.Tools.Utils;

namespace Dotnet9.Tools.Middleware;

public class CookieMiddleware
{
    private readonly RequestDelegate _next;

    public CookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, GlobalConfigs globalConfig)
    {
        var cookies = context.Request.Cookies;
        globalConfig.Initialize(cookies);

        await _next(context);
    }
}