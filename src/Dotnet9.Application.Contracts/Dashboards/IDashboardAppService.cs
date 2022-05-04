namespace Dotnet9.Application.Contracts.Dashboards;

public interface IDashboardAppService
{
    Task<DashboardViewModel> GetDashboardAsync();
}