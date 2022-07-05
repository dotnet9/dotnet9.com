namespace Dotnet9.Web.ServiceExtensions;

public static class DbSetup
{
    public static readonly ILoggerFactory EfLogger = LoggerFactory.Create(builder =>
    {
        builder.AddFilter((category, level) =>
            category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
    });

    public static void AddDbSetup(this IServiceCollection services, DbSettings dbSettings)
    {
        services.AddDbContextPool<Dotnet9DbContext>(option =>
            option.UseNpgsql(dbSettings.ConnectionString!)
                .UseLoggerFactory(EfLogger));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}