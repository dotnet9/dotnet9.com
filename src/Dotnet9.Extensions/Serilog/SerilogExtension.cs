using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Dotnet9.Extensions.Serilog;

public static class SerilogExtension
{
    public static void AddSerilogSetup()
    {
        var logOutputTemplate =
            "{Timestamp:HH:mm:ss.fff zzz} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine}";
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Default", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
#if DEBUG
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
#endif
            .WriteTo.File($"{AppContext.BaseDirectory}Logs/d9.log",
                rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10240000,
                retainedFileCountLimit: 30, outputTemplate: logOutputTemplate)
            .CreateLogger();
    }
}