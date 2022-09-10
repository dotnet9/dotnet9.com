namespace Dotnet9.WebAPI.Domain.ActionLogs;

public class ActionLogDomainService
{
    public ActionLog AddActionLog(AddActionLogRequest request)
    {
        var id = Guid.NewGuid();
        return new ActionLog(id, request.UId, request.UA, request.OS, request.Browser, request.IP, request.Referer,
            request.AccessName, request.Original, request.Url, request.Controller, request.Action,
            request.Method, request.Arguments, request.Duration);
    }
}