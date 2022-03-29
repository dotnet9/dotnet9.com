using Dotnet9.Web.ServiceExtensions;
using Dotnet9.Web.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
GlobalVar.AssetsLocalPath = builder.Configuration["AssetsLocalPath"];
GlobalVar.AssetsRemotePath = builder.Configuration["AssetsRemotePath"];

builder.Services.AddDbSetup(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddAutoMapperSetup();
builder.Services.AddRepositorySetup();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();