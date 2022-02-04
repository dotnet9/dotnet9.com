using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v0.1.0",
        Title = "Dotnet9.API",
        Description = "¿ò¼ÜËµÃ÷ÎÄµµ",
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

app.UseAuthorization();

app.MapControllers();

app.Run();