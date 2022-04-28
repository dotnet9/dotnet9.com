using Dotnet9.AdminAPI;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Dotnet9.Extensions;
using Dotnet9.Extensions.Serilog;
using Dotnet9.Web;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

SerilogExtension.AddSerilogSetup();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.AddDotnet9Web();
builder.AddAdminAPI<Dotnet9DbContext>();
builder.AddExtensions();

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