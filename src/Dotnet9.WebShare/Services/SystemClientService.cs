namespace Dotnet9.WebShare.Services;

public class SystemClientService : ISystemClientService
{
    private readonly HttpClient _httpClient;
    private SiteInfoDto? _siteInfo;

    public SystemClientService([FromServices] IHttpClientFactory httpClientFactory,
        IOptions<ServiceCallerOptions> option)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(option.Value.BaseAddress);
    }

    public async Task<SiteInfoDto?> GetSiteInfoAsync()
    {
        return _siteInfo ??= await _httpClient.GetFromJsonAsync<SiteInfoDto>("/api/systems/site");
    }
}