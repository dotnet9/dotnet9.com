namespace Dotnet9.Application;

public abstract class BaseService<TEntity> : IDynamicApiController where TEntity : Entity<long>, ISoftDelete, IAvailability, new()
{
    private readonly ISqlSugarRepository<TEntity> _repository;

    protected BaseService(ISqlSugarRepository<TEntity> repository)
    {
        _repository = repository;
    }
    /// <summary>
    /// 删除信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("删除信息"), HttpDelete("delete")]
    public virtual async Task Delete(KeyDto dto)
    {
        bool success = await _repository.UpdateAsync(x => new TEntity()
        {
            DeleteMark = true
        }, x => x.Id == dto.Id);
        if (!success)
        {
            throw Oops.Bah("删除失败");
        }
        await ClearCache();
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("修改状态"), HttpPatch("setStatus")]
    public virtual async Task SetStatus(AvailabilityDto dto)
    {
        bool success = await _repository.UpdateAsync(x => new TEntity()
        {
            Status = dto.Status
        }, x => x.Id == dto.Id);
        if (!success)
        {
            throw Oops.Bah("修改失败");
        }
        await ClearCache();
    }

    /// <summary>
    /// 清除缓存
    /// </summary>
    /// <returns></returns>
    internal virtual Task ClearCache()
    {
        return Task.CompletedTask;
    }
}