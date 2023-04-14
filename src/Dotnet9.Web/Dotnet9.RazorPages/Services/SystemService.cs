

namespace Dotnet9.RazorPages.Services;

public class SystemService : ISystemService
{
    private readonly ICaller _caller;
    private SiteInfo? _siteInfo;

    public SystemService(ICaller caller)
    {
        _caller = caller;
    }

    public async Task<SiteInfo?> GetSiteInfoAsync()
    {
        return _siteInfo ??= await _caller.GetAsync<SiteInfo>("/api/system");
    }
}