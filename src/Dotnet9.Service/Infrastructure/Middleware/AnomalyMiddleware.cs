namespace Dotnet9.Service.Infrastructure.Middleware;

public class AnomalyMiddleware : IMiddleware
{
    private readonly ILogger<AnomalyMiddleware> _logger;

    public AnomalyMiddleware(ILogger<AnomalyMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UserFriendlyException exception)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new
            {
                message = exception.Message
            });
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.StatusCode = 401;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "请求发生错误");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                message = "服务器发生错误"
            });
        }
    }
}