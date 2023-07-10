using System.Net.Http.Headers;

namespace Dotnet9.Caller.Middlewares;

public class RealIpMiddleware : ICallerMiddleware
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RealIpMiddleware([FromServices] IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task HandleAsync(MasaHttpContext masaHttpContext, CallerHandlerDelegate next,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (!masaHttpContext.RequestMessage.Headers.Contains(HttpContextExtensions.XForwardedForKey) &&
            _httpContextAccessor.HttpContext != null)
        {
            masaHttpContext.RequestMessage.Headers.Add(HttpContextExtensions.XForwardedForKey,
                _httpContextAccessor.HttpContext.GetClientIp());
        }

        return next();
    }
}