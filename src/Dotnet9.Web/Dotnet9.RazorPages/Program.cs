var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddCaller(callerBuilder =>
{
    callerBuilder.UseHttpClient(clientConfigure => clientConfigure.BaseAddress = "http://localhost:5000");
});
builder.Services.AddSingleton<ISystemService, SystemService>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();