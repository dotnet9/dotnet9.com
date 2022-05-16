using System.Text.Encodings.Web;
using System.Text.Unicode;
using Dotnet9.Extensions;
using Dotnet9.Extensions.CountSystemInfo;
using Dotnet9.Extensions.Repository;
using Dotnet9.Web.AutoMapper;
using Dotnet9.Web.Caches;
using Dotnet9.Web.Filters;
using Dotnet9.Web.Serilog;
using Dotnet9.Web.ServiceExtensions;
using Dotnet9.Web.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

SerilogExtension.AddSerilogSetup();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews(opt => { opt.Filters.Add<GlobalExceptionFilter>(); });
builder.Services.AddSwaggerSetup();

GlobalVar.SiteDomain = builder.Configuration["SiteDomain"];
GlobalVar.AssetsLocalPath = builder.Configuration["AssetsLocalPath"];
GlobalVar.AssetsRemotePath = builder.Configuration["AssetsRemotePath"];

builder.Services.AddDbSetup(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
builder.Services.AddAutoMapperSetup();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddCacheSetup(builder.Configuration.GetSection("Cache").Get<CacheConfig>()!);
builder.Services.AddRepositorySetup();
builder.Services.ConfigureNonBreakingSameSiteCookies();
PerfCounter.Init();


var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet9 API v1"));
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();