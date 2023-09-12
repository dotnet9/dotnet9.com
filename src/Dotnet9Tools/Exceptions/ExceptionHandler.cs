using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dotnet9Tools.Exceptions;

/// <summary>
///     异常处理
/// </summary>
public static class ExceptionHandler
{
    /// <summary>
    ///     全局拦截
    /// </summary>
    /// <param name="app"></param>
    public static void UseEx(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                IExceptionHandlerPathFeature? exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();
                ILogger<Exception>? log =
                    context.RequestServices.GetService<ILogger<Exception>>();
                log?.LogError(exceptionHandlerPathFeature?.Error.Message, exceptionHandlerPathFeature?.Error);
                if (exceptionHandlerPathFeature?.Error is UserException userEx)
                {
                    context.Response.StatusCode = userEx!.Code;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = userEx.Message
                    });
                }
                else
                {
                    if (context.Request.ContentType != null && context.Request.ContentType.Contains("json"))
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = "服务器处理出错了.."
                        });
                    }
                    else
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync("<h1>服务器内部错误</h1>");
                    }
                }
            });
        });
    }
}