namespace Dotnet9.Caller.Services.Abouts;

public class AboutService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/abouts";
    protected override string BaseAddress { get; set; }

    public AboutService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<AboutDto?> GetAsync()
    {
        return await Caller.GetAsync<AboutDto?>("");
    }
}