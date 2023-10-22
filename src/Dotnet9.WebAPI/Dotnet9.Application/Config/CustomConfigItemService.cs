using Dotnet9.Application.Config.Dtos;

namespace Dotnet9.Application.Config;

/// <summary>
/// 自定义配置项
/// </summary>
public class CustomConfigItemService : BaseService<CustomConfigItem>
{
    private readonly ISqlSugarRepository<CustomConfigItem> _repository;
    private readonly IEasyCachingProvider _easyCachingProvider;

    public CustomConfigItemService(ISqlSugarRepository<CustomConfigItem> repository,
        IEasyCachingProvider easyCachingProvider) : base(repository)
    {
        _repository = repository;
        _easyCachingProvider = easyCachingProvider;
    }

    /// <summary>
    /// 自定义配置项分页列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [DisplayName("自定义配置项分页列表")]
    public async Task<PageResult<JObject>> Page([FromQuery] CustomConfigItemQueryInput dto)
    {
        var result = await _repository.AsQueryable().Where(x => x.ConfigId == dto.Id)
            .Select(x => new { x.Id, x.Json, x.Status, x.CreatedTime }).ToPagedListAsync(dto);
        var list = result.Rows.Select(x =>
        {
            var o = JObject.Parse(x.Json);
            o["__Id"] = x.Id;
            o["__Status"] = (int)x.Status;
            o["__CreatedTime"] = x.CreatedTime;
            return o;
        }).ToList();
        return new PageResult<JObject>()
        {
            Rows = list,
            PageNo = result.PageNo,
            PageSize = result.PageSize,
            Pages = result.Pages,
            Total = result.Total
        };
    }

    /// <summary>
    /// 添加自定义配置子项
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加自定义配置子项")]
    [HttpPost("add")]
    public async Task AddItem(AddCustomConfigItemInput dto)
    {
        var item = dto.Adapt<CustomConfigItem>();
        await _repository.InsertAsync(item);
        await ClearCache();
    }

    /// <summary>
    /// 修改自定义配置子项
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("修改自定义配子置项")]
    [HttpPut("edit")]
    public async Task UpdateItem(UpdateCustomConfigItemInput dto)
    {
        var item = await _repository.GetByIdAsync(dto.Id);
        if (item == null) throw Oops.Bah("无效参数");
        dto.Adapt(item);
        await _repository.UpdateAsync(item);
        await ClearCache();
    }

    internal override Task ClearCache()
    {
        return _easyCachingProvider.RemoveByPrefixAsync(CacheConst.ConfigCacheKey);
    }
}