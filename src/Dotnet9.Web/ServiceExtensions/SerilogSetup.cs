using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace Dotnet9.Web.ServiceExtensions;

public static class SerilogSetup
{
    public static void AddSerilogSetup()
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Default", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
#if DEBUG
            .WriteTo.Console(new ExpressionTemplate("{ {Time: @t, Level: @l, Message: @m, Properties: @p} }\n\n",
                theme: TemplateTheme.Code))
#endif
            .WriteTo.File(new CompactJsonFormatter(), $"{AppContext.BaseDirectory}Logs/d9.log",
                rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10240000,
                retainedFileCountLimit: 30)
            .CreateLogger();
    }
}