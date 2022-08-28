namespace Dotnet9.Web.Filters;

public class RateLimitFilter : IAsyncActionFilter
{
    private readonly IMemoryCache _memCache;

    public RateLimitFilter(IMemoryCache memCache)
    {
        _memCache = memCache;
    }

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        if (remoteIp == null)
        {
            return Task.CompletedTask;
        }

        var cacheKey = $"LastVisitTick_{remoteIp}";
        var lastTick = _memCache.Get<long?>(cacheKey);
        if (lastTick == null || Environment.TickCount64 - lastTick > 1000)
        {
            _memCache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10));
            return next();
        }

        context.Result = new ContentResult { StatusCode = (int)HttpStatusCode.Conflict };
        return Task.CompletedTask;
    }
}