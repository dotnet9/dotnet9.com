using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.Web.ServiceExtensions;

public static class DbSetup
{
    public static readonly ILoggerFactory efLogger = LoggerFactory.Create(builder =>
    {
        builder.AddFilter((category, level) =>
            category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
    });

    public static void AddDbSetup(this IServiceCollection services, string connectionStr)
    {
        services.AddDbContextPool<Dotnet9DbContext>(option =>
            option.UseNpgsql(connectionStr)
                .UseLoggerFactory(efLogger));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}