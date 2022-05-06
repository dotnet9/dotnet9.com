using Dotnet9.Application.Contracts.ActionLogs;

namespace Dotnet9.Application.Contracts.Dashboards;

public class ActionLogViewModel
{
    public List<ActionLogDto>? ActionLogDtos { get; set; }
    public int Total { get; set; }
}