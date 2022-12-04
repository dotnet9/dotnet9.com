using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "Dotnet9.Web",
    LogFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}Dotnet9.Web.log"
});

var siteOption = builder.Configuration.GetSection("Site").Get<SiteOptions>();
if (siteOption?.ApiService != null)
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(siteOption.ApiService) });
}

builder.Services.AddRazorPages();
builder.Services.AddDataProtection();
//登录、注册的项目除了要启用WebApplicationBuilderExtensions中的初始化之外，还要如下的初始化
//不要用AddIdentity，而是用AddIdentityCore
//因为用AddIdentity会导致JWT机制不起作用，AddJwtBearer中回调不会被执行，因此总是Authentication校验失败
//https://github.com/aspnet/Identity/issues/1376
var idBuilder = builder.Services.AddIdentityCore<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        //不能设定RequireUniqueEmail，否则不允许邮箱为空
        //options.User.RequireUniqueEmail = true;
        //以下两行，把GenerateEmailConfirmationTokenAsync验证码缩短
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    }
);
idBuilder = new IdentityBuilder(idBuilder.UserType, typeof(Role), builder.Services);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
idBuilder.AddEntityFrameworkStores<Dotnet9DbContext>().AddDefaultTokenProviders()
    .AddRoleValidator<RoleValidator<Role>>()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<IdUserManager>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseDotnet9Default();
app.MapRazorPages();

app.Run();