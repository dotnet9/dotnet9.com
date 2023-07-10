namespace Dotnet9.ASPNETCore;

public static class ServiceCollectionExtensions
{
    public static void AddCache(this WebApplicationBuilder builder)
    {
        //Redis的配置
        var redisConnStr = builder.Configuration.GetValue<string>("ConnectionStrings:Redis");
        IConnectionMultiplexer redisConnMultiplexer = ConnectionMultiplexer.Connect(redisConnStr);
        builder.Services.AddSingleton(typeof(IConnectionMultiplexer), redisConnMultiplexer);
        builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
        builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();
    }

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}