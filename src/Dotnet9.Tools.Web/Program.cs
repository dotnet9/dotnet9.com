using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMasaBlazor();
builder.Services.AddMasaI18nForServer("wwwroot/locale");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");


app.UseStaticFiles();

app.UseRouting();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();