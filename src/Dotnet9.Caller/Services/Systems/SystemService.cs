namespace Dotnet9.Caller.Services.Systems;

public class SystemService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/systems";
    protected override string BaseAddress { get; set; }

    public SystemService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<SiteInfoDto?> GetAsync()
    {
        return await Caller.GetAsync<SiteInfoDto>("site");
    }

    public async Task<SitemapInfo?> GetSiteMapAsync()
    {
        return await Caller.GetAsync<SitemapInfo>("sitemap");
    }
}