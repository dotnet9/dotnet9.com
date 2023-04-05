namespace Dotnet9.Service.Infrastructure.Middleware;

public class LoggingMiddleware<TEvent> : EventMiddleware<TEvent>
    where TEvent : notnull, IEvent
{
    private readonly ILogger<LoggingMiddleware<TEvent>> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware<TEvent>> logger)
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