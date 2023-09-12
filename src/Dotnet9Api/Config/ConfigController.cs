using Dotnet9.Services.Config;
using Dotnet9.Services.Config.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9Api.Config;

public class ConfigController : BaseAdminController
{
    private readonly ConfigService _config;

    public ConfigController(ConfigService config)
    {
        _config = config;
    }

    [HttpGet]
    public async Task Test()
    {
        await _config.UpdateConfig(new Dictionary<string, string> { { "test-1", "mb" } });
    }

    /// <summary>
    ///     保存配置
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task SetSite(SiteConfig config)
    {
        Dictionary<string, string> kv = config.GetKv();
        await _config.UpdateConfig(kv);
    }

    /// <summary>
    ///     获取站点配置
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<SiteConfig> GetSiteConfig()
    {
        return await _config.Get<SiteConfig>();
    }
}