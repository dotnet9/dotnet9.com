using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9Tools.EFCore;

public static class EFExtensions
{
    /// <summary>
    ///     添加EFDbContext
    /// </summary>
    /// <typeparam name="TDB"></typeparam>
    /// <param name="services"></param>
    /// <param name="opt"></param>
    public static void AddEFCore<TDB>(this IServiceCollection services, Action<DbContextOptionsBuilder> opt)
        where TDB : DbContext
    {
        services.AddScoped<TranContext>();
        services.AddDbContext<TDB>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            opt(options);
        });
        services.AddScoped<DbContext>(a => { return a.GetService<TDB>()!; });
    }


    public static void AddPgSql<DB>(this IServiceCollection service, IConfiguration configuration) where DB : DbContext
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        service.AddDbContext<DB>(opt => { opt.UseNpgsql(configuration.GetConnectionString("pgsql")); });
        service.AddScoped<DbContext>(a => a.GetService<DB>()!);
    }
}