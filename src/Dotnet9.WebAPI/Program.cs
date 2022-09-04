using Dotnet9.WebAPI.Domain.UserAdmin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitializerOptions
{
    EventBusQueueName = "Dotnet9.WebAPI",
    LogFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}Dotnet9.WebAPI.log"
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Dotnet9.WebAPI", Version = "v1" });
    //c.AddAuthenticationHeader();
});
builder.Services.AddDataProtection();
//登录、注册的项目除了要启用WebApplicationBuilderExtensions中的初始化之外，还要如下的初始化
//不要用AddIdentity，而是用AddIdentityCore
//因为用AddIdentity会导致JWT机制不起作用，AddJwtBearer中回调不会被执行，因此总是Authentication校验失败
//https://github.com/aspnet/Identity/issues/1376
IdentityBuilder idBuilder = builder.Services.AddIdentityCore<User>(options =>
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
//idBuilder.AddEntityFrameworkStores<IdDbContext>().AddDefaultTokenProviders()
    //.AddRoleValidator<RoleValidator<Role>>()
    //.AddRoleManager<RoleManager<Role>>()
    //.AddUserManager<IdUserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();