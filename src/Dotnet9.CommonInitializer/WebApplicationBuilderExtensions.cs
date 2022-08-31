namespace Dotnet9.CommonInitializer;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureDbConfiguration(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureAppConfiguration((hostCtx, configBuilder) =>
        {
            //不能使用ConfigureAppConfiguration中的configBuilder去读取配置，否则就循环调用了，因此这里直接自己去读取配置文件
            //var configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //string connStr = configRoot.GetValue<string>("DefaultDB:ConnStr");
            var connStr = builder.Configuration.GetValue<string>("DefaultDB:ConnStr");

            configBuilder.AddDbConfiguration(() => new NpgsqlConnection(connStr), reloadOnChange: true,
                reloadInterval: TimeSpan.FromSeconds(5));
        });
    }
}