namespace Dotnet9.WebAPI.Service.Infrastructure.Middleware;

public class LogMiddleware<TEvent> : EventMiddleware<TEvent>
    where TEvent : notnull, IEvent
{
    private readonly ILogger<LogMiddleware<TEvent>> _logger;

    public LogMiddleware(ILogger<LogMiddleware<TEvent>> logger)
    {
        _logger = logger;
    }

    public override async Task HandleAsync(TEvent action, EventHandlerDelegate next)
    {
        var typeName = action.GetType().FullName;

        _logger.LogInformation("----- command {CommandType}", typeName);

        await next();
    }
}