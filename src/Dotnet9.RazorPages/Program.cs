var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();
builder.Services.AddServerSideBlazor();
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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStatusCodePagesWithReExecute("/Errors/{0}");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();