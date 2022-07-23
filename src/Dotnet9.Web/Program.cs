SerilogExtension.AddSerilogSetup();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
    opt.Filters.Add<RateLimitFilter>();
});
builder.Services.AddSwaggerSetup();

var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json", false, true);
configBuilder.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
var config = configBuilder.Build();
builder.Services.AddOptions().Configure<SiteSettings>(e => config.GetSection("Site").Bind(e))
    .Configure<CacheSettings>(e => config.GetSection("Cache").Bind(e))
    .Configure<DbSettings>(e => config.GetSection("DB").Bind(e))
    .Configure<JwtSettings>(e => config.GetSection("JWT").Bind(e));

builder.Services.AddDbSetup(builder.Configuration.GetSection("DB").Get<DbSettings>()!);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
builder.Services.AddAutoMapperSetup();

var jwtSettings = builder.Configuration.GetSection("JWT").Get<JwtSettings>()!;
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = jwtSettings.ValidAudience,
            ValidIssuer = jwtSettings.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret!))
        };
    });
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