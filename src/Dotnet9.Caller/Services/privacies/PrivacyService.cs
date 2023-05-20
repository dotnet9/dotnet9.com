namespace Dotnet9.Caller.Services.privacies;

public class PrivacyService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/privacies";
    protected override string BaseAddress { get; set; }

    public PrivacyService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<PrivacyDto?> GetAsync()
    {
        return await Caller.GetAsync<PrivacyDto>("");
    }
}