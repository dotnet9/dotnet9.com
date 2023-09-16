using Dotnet9.Services.Config;
using Dotnet9.Services.Config.Services;
using Dotnet9Tools.Configs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dotnet9Api.Config;

public partial class ConfigController : BaseAdminController
{
    private readonly ConfigService _config;
    private readonly Dotnet9DbContext _dbContext;

    public ConfigController(ConfigService config,  Dotnet9DbContext dbContext)
    {
        _config = config;
        _dbContext = dbContext;
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
    /// 生成种子数据
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task Seed()
    {
        Console.WriteLine($"创建数据库表结构：数据库如果不存在，EnsureCreatedAsync会根据{nameof(Dotnet9DbContext)}配置创建");
        await CreateDatabaseAsync();

        Console.WriteLine(
            $"添加种子数据，需要先将数据从 https://github.com/dotnet9/Assets.Dotnet9 克隆，并配置到 {AssetsLocalPath}");
        await CreateSeedDataAsync();
    }

    private async Task CreateDatabaseAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
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