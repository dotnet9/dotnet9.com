namespace Dotnet9.Web.ViewModels.Logins;

public record LoginResult(bool IsOk, ProcessInfo[]? Processes = null);