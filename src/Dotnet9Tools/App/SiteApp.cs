using System.Reflection;
using Dotnet9Tools.Auth;
using Dotnet9Tools.Configs;
using Dotnet9Tools.Exceptions;
using Dotnet9Tools.MiddleWare;
using Dotnet9Tools.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dotnet9Tools.App;

public class SiteApp
{
    public static void Run(string[] args, Action<IServiceCollection, IConfiguration>? action,
        Action<WebApplication>? appAction = null)
    {
        ILogger logger = SerilogExtensions.Instance();
        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddLog();
            IServiceCollection services = builder.Services;

            services.InjectSuffix(Assembly.Load("Dotnet9.Services"));
            services.InjectSuffix(Assembly.Load("Dotnet9Api"));
            services.InjectSuffix(Assembly.Load("Dotnet9.Repositoies"), "Repository");
            services.AddScoped<IApp, WebApp>();
            services.AddWebApiConfig();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            builder.AddApplication();
            builder.Services.AddCookieAuth();
            action?.Invoke(services, builder.Configuration);
            WebApplication app = builder.Build();


            // Configure the HTTP request pipeline.
            Console.WriteLine("当前环境:" + app.Environment.EnvironmentName);
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // var option = new RewriteOptions();
            // option.AddRedirectToNonWwwPermanent();
            // option.AddRedirectToHttps();
            // app.UseRewriter(option);
            app.UseStaticFiles();
            app.UseEx();
            app.UseSiteUid();
            app.UseCookieAuth();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();
            appAction?.Invoke(app);
            app.Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "程序已经停止");
            throw;
        }
    }
}