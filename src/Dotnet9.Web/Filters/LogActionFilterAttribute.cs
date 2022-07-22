namespace Dotnet9.Web.Filters;

public class LogActionFilterAttribute : ActionFilterAttribute
{
    private static readonly string[] IgnoreActionNames =
    {
        "Dotnet9.Web.Controllers.DashboardController.GetActionLogs (Dotnet9.Web)",
        "Dotnet9.Web.Controllers.DashboardController.Count (Dotnet9.Web)",
        "Dotnet9.Web.Controllers.AccountController.Login (Dotnet9.Web)",
        "Dotnet9.Web.Controllers.AccountController.CheckLogin (Dotnet9.Web)"
    };

    private static readonly string[] IgnoreIPs = { "::1" };
    private readonly IActionLogRepository _actionLogRepository;
    private readonly ILogger<LogActionFilterAttribute> _logger;
    private bool _isNotLog;

    public LogActionFilterAttribute(ILogger<LogActionFilterAttribute> logger, IActionLogRepository actionLogRepository)
    {
        _logger = logger;
        _actionLogRepository = actionLogRepository;
    }

    private string? ActionArguments { get; set; }
    private Stopwatch? Stopwatch { get; set; }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var ip = context.HttpContext.GetClientIP();
        var actionName = context.ActionDescriptor.DisplayName;

        if (IgnoreIPs.Contains(ip))
            _isNotLog = true;
        else if (IgnoreActionNames.Contains(actionName)) _isNotLog = true;

        if (_isNotLog) return;

        ActionArguments = JsonConvert.SerializeObject(context.ActionArguments);
        Stopwatch = new Stopwatch();
        Stopwatch.Start();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        if (_isNotLog) return;

        Stopwatch?.Stop();

        if (context.HttpContext.Request.Cookies.TryGetValue("id", out var uid) == false)
        {
            uid = Guid.NewGuid().ToString("N");
            context.HttpContext.Response.Cookies.Append("id", uid);
        }

        context.HttpContext.Request.Headers.TryGetValue("user-agent", out var ua);
        var ip = context.HttpContext.GetClientIP();
        context.HttpContext.Request.Headers.TryGetValue("referer", out var referer);
        var url = context.HttpContext.Request.Path.Value;

        var parser = Parser.GetDefault().Parse(ua);

        _ = _actionLogRepository.InsertAsync(new ActionLog
        {
            UId = uid,
            UA = ua,
            OS = parser.OS.ToString(),
            Browser = parser.UA.ToString(),
            Original = context.HttpContext.Request.Headers["Origin"].FirstOrDefault(),
            IP = ip,
            Referer = referer,
            Url = context.HttpContext.Request.Host + context.HttpContext.Request.Path +
                  context.HttpContext.Request.QueryString,
            Controller = context.Controller.ToString(),
            Action = context.ActionDescriptor.DisplayName,
            Method = context.HttpContext.Request.Method,
            Arguments = ActionArguments,
            Duration = Stopwatch!.Elapsed.TotalMilliseconds,
            CreateDate = DateTimeOffset.Now
        }).Result;
    }
}