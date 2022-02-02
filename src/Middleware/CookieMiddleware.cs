using Dotnet9.Utils;
using Microsoft.AspNetCore.Http;

namespace Dotnet9.Middleware;

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