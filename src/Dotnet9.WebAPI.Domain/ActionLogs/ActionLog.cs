namespace Dotnet9.WebAPI.Domain.ActionLogs;

public record ActionLog : IEntity, IHasCreationTime
{
    private ActionLog()
    {
    }

    internal ActionLog(Guid id,
        string uid,
        string ua,
        string os,
        string browser,
        string ip,
        string? referer = null,
        string? accessName = null,
        string? original = null,
        string? url = null,
        string? controller = null,
        string? action = null,
        string? method = null,
        string? arguments = null,
        double duration = 0)
    {
        Id = id;
        ChangeUId(uid);
        ChangeUa(ua);
        ChangeOs(os);
        ChangeBrowser(browser);
        ChangeIp(ip);
        ChangeReferer(referer);
        ChangeAccessName(accessName);
        ChangeOriginal(original);
        ChangeUrl(url);
        ChangeController(controller);
        ChangeAction(action);
        ChangeMethod(method);
        ChangeArguments(arguments);
        ChangeDuration(duration);
    }

    public string UId { get; set; } = null!;
    public string Ua { get; set; } = null!;
    public string Os { get; set; } = null!;
    public string Browser { get; set; } = null!;
    public string? Referer { get; set; }
    public string? AccessName { get; set; }
    public string? Original { get; set; }
    public string Ip { get; set; } = null!;
    public string? Url { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Method { get; set; }
    public string? Arguments { get; set; }
    public double Duration { get; set; }

    public Guid Id { get; protected set; } = Guid.NewGuid();

    public object[] GetKeys()
    {
        return new object[] { Id };
    }

    public DateTime CreationTime { get; internal set; } = DateTime.Now;


    public ActionLog ChangeUId(string uid)
    {
        UId = Check.NotNullOrWhiteSpace(uid, nameof(uid), ActionLogConsts.MaxUIdLength, ActionLogConsts.MinUIdLength);
        return this;
    }

    public ActionLog ChangeUa(string ua)
    {
        Ua = Check.NotNullOrWhiteSpace(ua, nameof(ua), ActionLogConsts.MaxUALength, ActionLogConsts.MinUALength);
        return this;
    }

    public ActionLog ChangeOs(string os)
    {
        Os = Check.NotNullOrWhiteSpace(os, nameof(os), ActionLogConsts.MaxOSLength, ActionLogConsts.MinOSLength);
        return this;
    }

    public ActionLog ChangeBrowser(string browser)
    {
        Browser = Check.NotNullOrWhiteSpace(browser, nameof(browser), ActionLogConsts.MaxBrowserLength,
            ActionLogConsts.MinBrowserLength);
        return this;
    }

    public ActionLog ChangeReferer(string? referer)
    {
        Referer = Check.Length(referer, nameof(referer), ActionLogConsts.MaxRefererLength);
        return this;
    }

    public ActionLog ChangeAccessName(string? accessName)
    {
        AccessName = Check.Length(accessName, nameof(accessName), ActionLogConsts.MaxAccessName);
        return this;
    }

    public ActionLog ChangeOriginal(string? original)
    {
        Original = Check.Length(original, nameof(original), ActionLogConsts.MaxOriginalLength);
        return this;
    }

    public ActionLog ChangeIp(string ip)
    {
        Ip = Check.NotNullOrWhiteSpace(ip, nameof(ip), ActionLogConsts.MaxIPLength, ActionLogConsts.MinIPLength);
        return this;
    }

    public ActionLog ChangeUrl(string? url)
    {
        Url = Check.Length(url, nameof(url), ActionLogConsts.MaxUrlLength);
        return this;
    }

    public ActionLog ChangeController(string? controller)
    {
        Controller = Check.Length(controller, nameof(controller), ActionLogConsts.MaxControllerLength);
        return this;
    }

    public ActionLog ChangeAction(string? action)
    {
        Action = Check.Length(action, nameof(action), ActionLogConsts.MaxActionLength);
        return this;
    }

    public ActionLog ChangeMethod(string? method)
    {
        Method = Check.Length(method, nameof(method), ActionLogConsts.MaxMethodLength);
        return this;
    }

    public ActionLog ChangeArguments(string? arguments)
    {
        Arguments = Check.Length(arguments, nameof(arguments), ActionLogConsts.MaxArgumentsLength);
        return this;
    }

    public ActionLog ChangeDuration(double duration)
    {
        Duration = duration;
        return this;
    }
}