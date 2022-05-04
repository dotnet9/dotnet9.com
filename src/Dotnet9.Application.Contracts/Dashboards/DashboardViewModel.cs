namespace Dotnet9.Application.Contracts.Dashboards;

public class DashboardViewModel
{
    public int PostCount { get; set; }

    public int IPOf24Hours { get; set; }

    public int NotFoundRequestIn24Hours { get; set; }
}