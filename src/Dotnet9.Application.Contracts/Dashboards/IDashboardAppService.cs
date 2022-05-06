namespace Dotnet9.Application.Contracts.Dashboards;

public interface IDashboardAppService
{
    Task<DashboardViewModel> GetDashboardAsync();

    Task<ActionLogViewModel> GetActionLogAsync(int page);
}