using Dotnet9.Application.Logging.Dtos;

namespace Dotnet9.Application.Logging;
/// <summary>
/// 登录日志
/// </summary>
public class SysSigninLogService : IDynamicApiController
{
    private readonly ISqlSugarRepository<SysSigninLog> _repository;

    public SysSigninLogService(ISqlSugarRepository<SysSigninLog> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 登录日志分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("登录日志列表")]
    [HttpGet]
    public async Task<PageResult<SigninLogPageOutput>> List([FromQuery] LogPageQueryInput dto)
    {
        return await _repository.AsQueryable().InnerJoin<SysUser>((log, user) => log.UserId == user.Id)
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Keyword), log => log.Message.Contains(dto.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Account), (log, user) => user.Account.Contains(dto.Account))
            .OrderByDescending(log => log.Id)
            .Select((log, user) => new SigninLogPageOutput
            {
                Id = log.Id,
                Message = log.Message,
                RemoteIp = log.RemoteIp,
                Location = log.Location,
                OsDescription = log.OsDescription,
                UserAgent = log.UserAgent,
                CreatedTime = log.CreatedTime,
                Account = user.Account
            }).ToPagedListAsync(dto);

    }
}