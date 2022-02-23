using System.Globalization;
using Dotnet9.Tools.Web.Middleware;
using Dotnet9.Tools.Web.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<I18n>();
builder.Services.AddScoped<GlobalConfigs>();
builder.Services.AddScoped<CookieStorage>();
builder.Services.AddMasaBlazor();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");


app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<CookieMiddleware>();
app.UseRequestLocalization(opts =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new("zh-CN"),
        new("en-US")
    };
    opts.SupportedCultures = supportedCultures;
    opts.SupportedUICultures = supportedCultures;
});
I18nHelper.AddLang();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();