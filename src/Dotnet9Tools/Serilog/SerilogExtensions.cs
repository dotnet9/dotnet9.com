using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Dotnet9Tools.Serilog;

public static class SerilogExtensions
{
    public static ILogger Instance()
    {
        string path = Directory
            .GetCurrentDirectory(); //GetDirectoryName((new SerilogExtensions()).GetType().Assembly?.Location);
        Console.WriteLine("获取当前路径:" + path);
        path = Path.Combine(path!, "Logs");
        string infoPath = Path.Combine(path, "Info", "info_.log");
        string errPath = Path.Combine(path, "Error", "err_.log");
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Logger(x =>
                x.Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Information).WriteTo
                    .File(infoPath, rollingInterval: RollingInterval.Day)
            )
            .WriteTo.Logger(x =>
                x.Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Error).WriteTo
                    .File(errPath, rollingInterval: RollingInterval.Day)
            )
            .CreateLogger();
        return Log.Logger;
    }

    public static void AddLog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();
    }
}