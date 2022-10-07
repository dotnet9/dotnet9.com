using Dotnet9.Application.Contracts.ActionLogs;

namespace Dotnet9.Application.Contracts.Dashboards;

public class DashboardViewModel
{
    public SystemCountDto? SystemCountInfo { get; set; }

    public LatestActionLogDto? LatestLogs { get; set; }

    public Top10SearchDto? Top10Searches { get; set; }

    public Top10AccessPageDto? Top10AccessPages { get; set; }
}