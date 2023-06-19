namespace Dotnet9.WebShare;

public static class ServiceCollectionExtensions
{
    public static void AddFrontWebShare(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ISystemClientService, SystemClientService>();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddMasaBlazor();


        var config = builder.Configuration;
        var configOption = new ServiceCallerOptions();
        var callerSection = config.GetSection("ServiceCaller");
        builder.Services.Configure<ServiceCallerOptions>(callerSection);
        callerSection.Bind(configOption);

        builder.Services.AddDotnet9ApiGateways();

        builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
    }
}