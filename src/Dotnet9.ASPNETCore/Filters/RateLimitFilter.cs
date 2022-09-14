namespace Dotnet9.ASPNETCore.Filters;

public class RateLimitFilter : IAsyncActionFilter
{
    private readonly IMemoryCacheHelper _cacheHelper;

    public RateLimitFilter(IMemoryCacheHelper cacheHelper)
    {
        _cacheHelper = cacheHelper;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var remoteIp = context.HttpContext.GetClientIP();
        if (remoteIp == null)
        {
            return;
        }

        var isFirst = false;

        async Task<long> GetLastVisitTick()
        {
            isFirst = true;
            return await Task.FromResult(Environment.TickCount64);
        }

        var lastTick = await _cacheHelper.GetOrCreateAsync($"RateLimitFilter_OnActionExecutionAsync_{remoteIp}",
            async e => await GetLastVisitTick());
        if (isFirst || Environment.TickCount64 - lastTick > 1000)
        {
            await next();
        }

        context.Result = new ContentResult { StatusCode = (int)HttpStatusCode.Conflict, Content = "访问过于频繁" };
    }
}