namespace Dotnet9.ASPNETCore.Filters;

public class GlobalExceptionFilter : IAsyncExceptionFilter
{
    private readonly IHostingEnvironment _env;
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IHostingEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var message = _env.IsDevelopment() ? context.Exception.ToString() : "程序中出现未处理异常";
        var result = new ObjectResult(new
        {
            code = (int)HttpStatusCode.InternalServerError, message
        })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
        context.Result = result;
        context.ExceptionHandled = true;
        _logger.LogError(context.Exception, message);
        await Task.CompletedTask;
    }
}