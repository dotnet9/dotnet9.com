namespace Dotnet9.Service.Domain.Aggregates.ActionLogs;

public class ActionLogManager
{
    public ActionLog Create(string uid, string? ua, string os, string browser, string ip, string? referer = null,
        string? accessName = null,
        string? original = null,
        string? url = null,
        string? controller = null,
        string? action = null,
        string? method = null,
        string? arguments = null,
        double duration = 0)
    {
        var id = Guid.NewGuid();

        return new ActionLog(id, uid, ua, os, browser, ip, referer,
            accessName, original, url, controller, action,
            method, arguments, duration);
    }
}