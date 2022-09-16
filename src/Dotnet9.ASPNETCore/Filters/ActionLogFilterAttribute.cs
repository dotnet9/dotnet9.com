namespace Dotnet9.ASPNETCore.Filters;

public class ActionLogFilterAttribute : ActionFilterAttribute
{
    private readonly ILogger<ActionLogFilterAttribute> _logger;
    private readonly IMediator _mediator;
    private string? _actionArguments;
    private Stopwatch? _stopwatch;

    public ActionLogFilterAttribute(ILogger<ActionLogFilterAttribute> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);


        _actionArguments = JsonSerializer.Serialize(context.ActionArguments);
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);


        _stopwatch!.Stop();

        if (context.HttpContext.Request.Cookies.TryGetValue("id", out var uid) == false)
        {
            uid = Guid.NewGuid().ToString("N");
            context.HttpContext.Response.Cookies.Append("id", uid);
        }

        context.HttpContext.Request.Headers.TryGetValue("user-agent", out var ua);
        var ip = context.HttpContext.GetClientIP();
        context.HttpContext.Request.Headers.TryGetValue("referer", out var referer);

        var parser = Parser.GetDefault().Parse(ua);

        var os = parser.OS.ToString();
        var browser = parser.UA.ToString();
        var original = context.HttpContext.Request.Headers["Origin"].FirstOrDefault();
        var url = context.HttpContext.Request.Host + context.HttpContext.Request.Path +
                  context.HttpContext.Request.QueryString;
        var controller = context.Controller.ToString();
        var action = context.ActionDescriptor.DisplayName;
        var method = context.HttpContext.Request.Method;
        var duration = _stopwatch!.Elapsed.TotalMilliseconds;
        var createDate = DateTimeOffset.Now;

        var actionLogCreatedEvent = new ActionLogCreatedEvent(uid, ua, os, browser, ip!, referer, null, original, url,
            controller, action, method, _actionArguments, duration);
        _mediator.Publish(actionLogCreatedEvent);
    }
}