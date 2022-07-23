using Microsoft.OpenApi.Models;

namespace Dotnet9.Web.ServiceExtensions;

public static class SwaggerSetup
{
    public static void AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddMvcCore(options => { options.Filters.Add(typeof(LogActionFilterAttribute)); }).AddApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Dotnet9 API",
                Description = "Dotnet9接口文档",
                TermsOfService = new Uri("https://dotnet9.com"),
                Contact = new OpenApiContact
                {
                    Name = "沙漠尽头的狼",
                    Url = new Uri("https://dotnet9.com")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://dotnet9.com")
                }
            });

            var mvcXmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dotnet9.Web.xml");
            options.IncludeXmlComments(mvcXmlFile, true);

            var scheme = new OpenApiSecurityScheme
            {
                Description = "Authorization header. \r\nExample: 'Bearer 354687354'",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization" },
                Scheme = "oauth2", Name = "Authorization",
                In = ParameterLocation.Header, Type = SecuritySchemeType.ApiKey
            };
            options.AddSecurityDefinition("Authorization", scheme);
            var requirement = new OpenApiSecurityRequirement
            {
                [scheme] = new List<string>()
            };
            options.AddSecurityRequirement(requirement);
        });
    }
}