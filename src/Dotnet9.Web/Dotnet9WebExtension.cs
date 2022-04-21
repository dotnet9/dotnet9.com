using Dotnet9.Extensions.Repository;
using Dotnet9.Web.ServiceExtensions;
using Dotnet9.Web.Utils;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Dotnet9.Web;

public static class Dotnet9WebExtension
{
    public static void AddDotnet9Web(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerSetup();

        GlobalVar.SiteDomain = builder.Configuration["SiteDomain"];
        GlobalVar.AssetsLocalPath = builder.Configuration["AssetsLocalPath"];
        GlobalVar.AssetsRemotePath = builder.Configuration["AssetsRemotePath"];

        builder.Services.AddDbSetup(builder.Configuration.GetConnectionString("DefaultConnection")!);
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
    }
}