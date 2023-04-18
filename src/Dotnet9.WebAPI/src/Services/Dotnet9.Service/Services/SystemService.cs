namespace Dotnet9.Service.Services;

public class SystemService : ServiceBase
{
    private IOptions<SiteOptions> Options { get; }

    public SystemService(IOptions<SiteOptions> options) : base("/api/systems")
    {
        Options = options;
    }

    public async Task<SiteInfo> GetAsync()
    {
        return await Task.FromResult(Options.Value.Map<SiteInfo>());
    }
}