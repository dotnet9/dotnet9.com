using Dotnet9.Application.Logging.Dtos;

namespace Dotnet9.Application.Logging;
/// <summary>
/// 操作日志
/// </summary>
public class SysOperationLogService : IDynamicApiController
{
    private readonly ISqlSugarRepository<SysOperationLog> _repository;

    public SysOperationLogService(ISqlSugarRepository<SysOperationLog> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 操作日志列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("系统操作日志列表")]
    [HttpGet]
    public async Task<PageResult<OperationLogPageOutput>> List([FromQuery] LogPageQueryInput dto)
    {
        return await _repository.AsQueryable().LeftJoin<SysUser>((log, user) => log.UserId == user.Id)
             .WhereIF(!string.IsNullOrWhiteSpace(dto.Keyword),
                 log => log.Description.Contains(dto.Keyword) || log.Message.Contains(dto.Keyword))
             .WhereIF(dto.LogLevel.HasValue, log => log.LogLevel == dto.LogLevel)
             .WhereIF(!string.IsNullOrWhiteSpace(dto.Account), (log, user) => user.Account.Contains(dto.Account))
             .OrderByDescending(log => log.Id)
             .Select((log, user) => new OperationLogPageOutput
             {
                 Id = log.Id,
                 Description = log.Description,
                 LogLevel = log.LogLevel,
                 Message = log.Message,
                 ActionName = log.ActionName,
                 ControllerName = log.ControllerName,
                 HttpMethod = log.HttpMethod,
                 HttpStatusCode = log.HttpStatusCode,
                 Elapsed = log.Elapsed,
                 Exception = log.Exception,
                 Location = log.Location,
                 RemoteIp = log.RemoteIp,
                 OsDescription = log.OsDescription,
                 UserAgent = log.UserAgent,
                 //Parameter = log.Parameter,
                 //Response = log.Response,
                 RequestUrl = log.RequestUrl,
                 Account = user.Account,
                 CreatedTime = log.CreatedTime
             }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 清除日志
    /// </summary>
    /// <returns></returns>
    [DisplayName("清除操作日志")]
    [HttpDelete("clear")]
    public async Task Clear()
    {
        await _repository.DeleteAsync(x => x.Id > 0);
    }
}