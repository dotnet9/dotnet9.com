using System.Globalization;
using BlazorComponent.I18n;
using Dotnet9.Tools.Web.Middleware;
using Dotnet9.Tools.Web.Utils;
using Ganss.XSS;
using CookieStorage = Dotnet9.Tools.Web.Utils.CookieStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<I18n>();
builder.Services.AddScoped<GlobalConfigs>();
builder.Services.AddScoped<CookieStorage>();
builder.Services.AddMasaBlazor();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(x =>
{
    // Configure sanitizer rules as needed here.
    // For now, just use default rules + allow class attributes
    var sanitizer = new HtmlSanitizer();
    sanitizer.AllowedAttributes.Add("class");
    return sanitizer;
});

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