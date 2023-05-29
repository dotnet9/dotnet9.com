namespace Dotnet9.Service.Infrastructure.Middleware;

public class AuditMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var ip = context.GetClientIp();
        var sw = Stopwatch.StartNew();
        try
        {
            await next(context);
        }
        finally
        {
            sw.Stop();
            Console.WriteLine($"IP:{ip} Date:{DateTime.Now:yyyy-MM-dd HH:mm:ss} Path:{context.Request.Path} time:{sw.ElapsedMilliseconds} ms");
        }
    }
}
