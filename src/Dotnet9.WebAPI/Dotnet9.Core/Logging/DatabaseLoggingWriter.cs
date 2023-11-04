using Dotnet9.Core.Const;
using Dotnet9.Core.Entities;
using Furion.Logging;
using Newtonsoft.Json;
using Easy.Core;

namespace Dotnet9.Core.Logging;

// 数据库日志
public class DatabaseLoggingWriter : IDatabaseLoggingWriter, IDisposable
{
    private readonly IServiceScope _serviceScope;
    private readonly ISqlSugarRepository<SysOperationLog> _sysOperationLogRepository;
    private readonly ISqlSugarRepository<SysSigninLog> _sysSigninLogRepository;
    private readonly ISqlSugarRepository<SysUser> _sysUseRepository;

    public DatabaseLoggingWriter(IServiceScopeFactory scopeFactory)
    {
        _serviceScope = scopeFactory.CreateScope();
        _sysOperationLogRepository =
            _serviceScope.ServiceProvider.GetRequiredService<ISqlSugarRepository<SysOperationLog>>();
        _sysSigninLogRepository = _serviceScope.ServiceProvider.GetRequiredService<ISqlSugarRepository<SysSigninLog>>();
        _sysUseRepository = _serviceScope.ServiceProvider.GetRequiredService<ISqlSugarRepository<SysUser>>();
    }

    // 文档地址：http://furion.baiqian.ltd/docs/logging#18114-json-%E6%A0%BC%E5%BC%8F
    public void Write(LogMessage logMsg, bool flush)
    {
        var contextJson = logMsg.Context.Get("loggingMonitor").ToString()!;
        var json = JsonConvert.DeserializeObject<dynamic>(contextJson);
        var ip = string.IsNullOrWhiteSpace(logMsg.Context.Get("ip").ToString())
            ? json.remoteIPv4.ToString()
            : logMsg.Context.Get("ip").ToString();
        var location = HttpContextExtension.GetGeolocation(ip);

        //记录登录日志
        if ("AuthService".Equals(json.controllerTypeName.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
            "SignIn".Equals(json.actionTypeName.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            string account = json.parameters[0].value.account;
            if (string.IsNullOrWhiteSpace(account))
            {
                return;
            }

            var id = _sysUseRepository.AsQueryable().Where(x => x.Account == account).Select<long>().First();
            if (id == 0)
            {
                return;
            }

            var sysSigninLog = new SysSigninLog
            {
                OsDescription = $"{json.osDescription}（{json.osArchitecture}）",
                Location = location,
                RemoteIp = ip,
                UserAgent = json.userAgent,
                UserId = id,
                CreatedTime = logMsg.LogDateTime,
                Message = json.validation == null ? "登录成功" : json.validation.message
            };
            _sysSigninLogRepository.Insert(sysSigninLog);

            return;
        }

        //不记录操作日志列表查询和博客的操作记录
        if ("SysOperationLogService".Equals(json.controllerTypeName.ToString(),
                StringComparison.CurrentCultureIgnoreCase) &&
            "List".Equals(json.actionTypeName.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
            (json.controllerTypeName.ToString().EndsWith("Controller", StringComparison.CurrentCultureIgnoreCase) &&
             logMsg.Exception == null))
        {
            return;
        }

        //记录登录日志
        long? userId = null;
        if (json.authorizationClaims != null)
        {
            foreach (var item in json.authorizationClaims)
            {
                if (item.type != AuthClaimsConst.AuthIdKey) continue;
                userId = item.value;
                break;
            }
        }

        //记录操作日志
        var sysOperationLog = new SysOperationLog
        {
            Description = json.displayTitle,
            ActionName = json.actionTypeName,
            ControllerName = json.controllerTypeName,
            Elapsed = json.timeOperationElapsedMilliseconds,
            HttpMethod = json.httpMethod,
            HttpStatusCode = json.returnInformation.httpStatusCode,
            Message = logMsg.Message,
            RemoteIp = ip,
            RequestUrl = json.requestUrl,
            UserAgent = json.userAgent,
            ThreadId = logMsg.ThreadId,
            TraceId = json.traceId,
            UserId = userId,
            Response = json.returnInformation.value == null
                ? null
                : JsonConvert.SerializeObject(json.returnInformation.value),
            Parameter = json.parameters == null ? null : JsonConvert.SerializeObject(json.parameters),
            OsDescription = $"{json.osDescription}（{json.osArchitecture}）",
            Location = location,
            Exception = logMsg.Exception == null ? null : JsonConvert.SerializeObject(logMsg.Exception),
            LogLevel = logMsg.LogLevel,
            CreatedTime = logMsg.LogDateTime
        };
        _sysOperationLogRepository.Insert(sysOperationLog);
    }

    /// <summary>
    /// 释放服务作用域
    /// </summary>
    public void Dispose()
    {
        _serviceScope?.Dispose();
    }
}