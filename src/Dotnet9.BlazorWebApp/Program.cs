var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddHttpContextAccessor();

var config = builder.Configuration;
var configOption = new ServiceCallerOptions();
var callerSection = config.GetSection("ServiceCaller");
builder.Services.Configure<ServiceCallerOptions>(callerSection);
callerSection.Bind(configOption);

builder.Services.AddDotnet9ApiGateways();

builder.Services.AddSingleton<ISystemClientService, SystemClientService>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.MapRazorComponents<App>();

app.Run();