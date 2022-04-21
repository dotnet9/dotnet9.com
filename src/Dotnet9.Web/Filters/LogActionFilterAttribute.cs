using System.Diagnostics;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Web.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Dotnet9.Web.Filters;

public class LogActionFilterAttribute : ActionFilterAttribute
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly ILogger<LogActionFilterAttribute> _logger;

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

        ActionArguments = JsonConvert.SerializeObject(context.ActionArguments);
        Stopwatch = new Stopwatch();
        Stopwatch.Start();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        Stopwatch?.Stop();


        _ = _actionLogRepository.InsertAsync(new ActionLog
        {
            Original = context.HttpContext.Request.Headers["Origin"].FirstOrDefault(),
            IP = context.HttpContext.GetClientIP(),
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