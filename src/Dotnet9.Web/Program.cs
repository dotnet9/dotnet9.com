using System.Text.Encodings.Web;
using System.Text.Unicode;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ServiceExtensions;
using Dotnet9.Web.Utils;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

SerilogSetup.AddSerilogSetup();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerSetup();

GlobalVar.SiteDomain = builder.Configuration["SiteDomain"];
GlobalVar.AssetsLocalPath = builder.Configuration["AssetsLocalPath"];
GlobalVar.AssetsRemotePath = builder.Configuration["AssetsRemotePath"];
GlobalVar.Cache = builder.Configuration.GetSection("Cache").Get<CacheConfig>();

builder.Services.AddDbSetup(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddAutoMapperSetup();
builder.Services.AddRepositorySetup();
builder.Services.AddCacheSetup();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

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

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();