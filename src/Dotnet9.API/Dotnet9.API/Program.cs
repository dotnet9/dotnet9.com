using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dotnet9.Common.Helpers;
using Dotnet9.EntityFramework;
using Dotnet9.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => { b.RegisterModule(new AutofacModuleRegister()); });

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<Dotnet9Context>(option =>
{
    option.UseLazyLoadingProxies()
        .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection")!,
            MySqlServerVersion.LatestSupportedServerVersion,
            oo => oo.MigrationsAssembly("Dotnet9.EntityFramework"))
        .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddSingleton(new Appsettings(builder.Configuration));
builder.Services.AddAutoMapperSetup();

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v0.1.0",
        Title = "Dotnet9.API",
        Description = "框架说明文档",
        Contact = new OpenApiContact
        {
            Name = "Dotnet9",
            Email = "632871194@qq.com"
        }
    });

    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "Dotnet9.API.xml");
    c.IncludeXmlComments(xmlPath, true);

    var xmlModelsPath = Path.Combine(basePath, "Dotnet9.Models.xml");
    c.IncludeXmlComments(xmlModelsPath, true);

    c.OperationFilter<AddResponseHeadersFilter>();
    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

    c.OperationFilter<SecurityRequirementsOperationFilter>();
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT授权（数据将在请求头中进行传输）直接在下框中输入Bearer {token}{注意两者之间是一个空格}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var audienceConfig = builder.Configuration["Audience:Audience"];
    var symmetricKeyAsBase64 = builder.Configuration["Audience:Secret"];
    var iss = builder.Configuration["Audience:Issuer"];
    var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64!);
    var signingKey = new SymmetricSecurityKey(keyByteArray);
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,
        ValidateIssuer = true,
        ValidIssuer = iss,
        ValidateAudience = true,
        ValidAudience = audienceConfig,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true
    };
});

// [Authorize(Policy = "Admin")]
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
    options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

#region Swagger

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp v1");
    c.RoutePrefix = "";
});

#endregion

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();