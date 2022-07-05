SerilogExtension.AddSerilogSetup();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews(opt => { opt.Filters.Add<GlobalExceptionFilter>(); });
builder.Services.AddSwaggerSetup();

var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json", false, true);
var config = configBuilder.Build();
builder.Services.AddOptions().Configure<SiteSettings>(e => config.GetSection("Site").Bind(e))
    .Configure<CacheSettings>(e => config.GetSection("Cache").Bind(e))
    .Configure<DbSettings>(e => config.GetSection("DB").Bind(e));

builder.Services.AddDbSetup(builder.Configuration.GetSection("DB").Get<DbSettings>()!);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
builder.Services.AddAutoMapperSetup();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddCacheSetup(builder.Configuration.GetSection("Cache").Get<CacheSettings>()!);
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