using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.Web.ServiceExtensions;

public static class DbSetup
{
    public static void AddDbSetup(this IServiceCollection services, string connectionStr)
    {
        services.AddDbContextPool<Dotnet9DbContext>(option =>
            option.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
    }
}